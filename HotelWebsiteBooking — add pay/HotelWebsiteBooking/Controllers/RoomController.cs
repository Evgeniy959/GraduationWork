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
            ViewBag.TotalPrice = end.Subtract(start).Days;
            var rooms = _daoRoom.SearchAsync(start, end, count).Result;
            return rooms.Any() ? View(rooms) : RedirectToAction("NotFind");
        }

        public IActionResult Booking(int id, int price, Client client)
        {
            ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();
            ViewBag.Price = price;
            ViewBag.TotalPrice = _date.end.Subtract(_date.start).Days;

            client.RoomId = id;
            return View(client);
        }

        public IActionResult NotFind()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBooking(RoomDate date, Client client, Comment comment, string content, Order order, int price)
        {
            ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();
            ViewBag.Price = price;
            if (_daoRoom.AddBookingAsync(date, client, comment, content, order).Result == true)
            {
                //return View("Info", client);
                return View("Pay");
            }
            else
            {
                TempData["Status"] = "Failed booking, try again!";
                return View("Booking", client);
            }
        }
        
        [HttpPost]
        public IActionResult Pay(string stripeEmail, string stripeToken, int price)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = price*100,
                Description = "Test Payment",
                Currency = "usd",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string>()
                {
                    {"OrderId", "111" },
                    {"Postcode", "LEE111" },

                }
            });
            if (charge.Status == "succeeded")
            {
                string BalanceTransactoinId = charge.BalanceTransactionId;
                ViewBag.Balance = BalanceTransactoinId;
                ViewBag.Status = charge.Status;
                //return View();
                return View("InfoBalance");
            }
            else
            {

            }
            return View();
        }


    }
}
