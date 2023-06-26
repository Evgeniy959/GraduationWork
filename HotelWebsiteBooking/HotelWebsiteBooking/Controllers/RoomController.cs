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

        public IActionResult Booking(int id, Client client)
        {
            ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();
            client.RoomId = id;
            return View(client);
        }

        public IActionResult NotFind()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddBooking(RoomDate date, Client client, Comment comment, string content, Order order)
        {
            ViewBag.Start = _date.start.ToLongDateString();
            ViewBag.End = _date.end.ToLongDateString();
            if (_daoRoom.AddBookingAsync(date, client, comment, content, order).Result == true)
            {
                return View("Info", client);
            }
            else
            {
                TempData["Status"] = "Failed booking, try again!";
                return View("Booking", client);
            }
        }

    }
}
