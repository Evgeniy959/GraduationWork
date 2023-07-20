using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;
using HotelWebsiteBooking.Service.DateService;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HotelWebsiteBooking.Service.RoomService;
using Stripe;

namespace HotelWebsiteBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly DaoDate _date;
        private readonly IDaoRoom _daoRoom;

        public RoomController(AppDbContext context, DaoDate date, IDaoRoom daoRoom)
        {
            _context = context;
            _date = date;
            _daoRoom = daoRoom;
        }

        public async Task<IActionResult> Rooms(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Rooms.ToListAsync()).Count / 9.0);
            ViewBag.CurrentPage = page;

            return (await _context.Rooms.ToListAsync()).Any() ? 
                          View(_daoRoom.RoomsAsync(page).Result) : NotFound();

        }


        [HttpPost]
        public IActionResult Search(DateTime start, DateTime end, int count)
        {
            ViewBag.Days = end.Subtract(start).Days;
            var rooms = _daoRoom.SearchAsync(start, end, count).Result;
            return rooms.Any() ? View(rooms) : RedirectToAction("NotFind");
        }

        public IActionResult Tariff(int id, int price)
        {
            ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();
            ViewBag.Price = price;
            ViewBag.Days = _date.end.Subtract(_date.start).Days;
            _context.Categorys.Load();
            _context.Tariffs.Load();
            _context.TariffPlans.Load();
            var room = _context.Rooms.FirstOrDefault(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }
            //room.Id = id;
            return View(room);
        }

        public IActionResult Booking(int roomId, int tariffId, int price, string tariff, Client client)
        //public IActionResult Booking(int id, string tariff, RoomTariff roomtariff, Client client)
        {
            ViewBag.Start = _date.start.ToShortDateString();
            ViewBag.End = _date.end.ToShortDateString();
            ViewBag.Price = price;
            //ViewBag.Price = roomtariff.Price;
            ViewBag.Days = _date.end.Subtract(_date.start).Days;
            ViewBag.Tariff = tariff;
            //ViewBag.Tariff = tariff.TariffPlan.Description;
            _context.Rooms.Load();
            _context.Categorys.Load();
            var room = _context.Rooms.FirstOrDefault(m => m.Id == roomId);
            if (room == null)
            {
                return NotFound();
            }
            client.Room = room;
            client.RoomId = roomId;
            return View(client);
        }

        public IActionResult NotFind()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBooking(RoomDate date, Client client, Comment comment, string content, string payment, int price, Order order, OrderPayable orderPay)
        {
            /*ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();*/
            
            if (payment == "online")
            {
                ViewBag.Price = price;
                ViewBag.Сontent = content;
                ViewBag.Name = client.Name;
                ViewBag.Surname = client.Surname;
                ViewBag.Email = client.Email;
                ViewBag.Phone = client.Phone;
                ViewBag.RoomId = client.RoomId;
                return View("Pay");
            }
            else if (payment == "inPlace")
            {
                if (_daoRoom.AddBookingAsync(date, client, comment, content, order, orderPay).Result == true)
                {
                    return View("Info", client);
                }
                
            }
            //else
            //{
                TempData["Status"] = "Неудачное бронирование, попробуйте еще раз!";
                return View("Booking", client);
            //}
            //return View("Booking", client);
        }
        
        [HttpPost]
        public IActionResult Pay(string stripeEmail, string stripeToken, RoomDate date, Client client, Comment comment, string content, Order order, OrderPayable orderPay, int price)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                //Email = client.Email,
                //Name = client.Name,
                Source = stripeToken
            });
            var orderPayId = Guid.NewGuid();
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = price*100,
                Description = orderPayId.ToString(),
                Currency = "rub",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"Order number", orderPayId.ToString() },
                    {"Client email", client.Email },
                    {"Client name", client.Name },
                    {"Client surname", client.Surname }

                }
            });
            if (charge.Status == "succeeded")
            {
                /*string BalanceTransactoinId = charge.BalanceTransactionId;
                ViewBag.Balance = BalanceTransactoinId;*/
                ViewBag.Status = charge.Status;
                orderPay.Id = orderPayId;
                orderPay.Status = "оплачено";
                if (_daoRoom.AddBookingAsync(date, client, comment, content, order, orderPay).Result == true)
                {
                    //return View("Info", client);
                    //charge.Description = order.Id.ToString();
                    return View("InfoPay");
                }
                else
                {
                    TempData["Status"] = "Неудачное бронирование, попробуйте еще раз!";
                    return View("Pay", client);
                }
                //return View();
                //return View("InfoBalance");
            }
            else
            {
                TempData["Status"] = "Неудачная оплата, попробуйте еще раз!";
                return View("Pay", client);
            }
            //return View();
        }


    }
}
