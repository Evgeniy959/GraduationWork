﻿using HotelAdmin.Models;
using HotelAdmin.Service.OrderService;
using HotelAdmin.Service.RoomDateService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelAdmin.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoOrder _daoOrder;

        public OrderController(AppDbContext context, IDaoOrder daoOrder)
        {
            _context = context;
            _daoOrder = daoOrder;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Orders.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoOrder.IndexAsync(page).Result);
        }

        public IActionResult Delete(int id)
        {
            if (_daoOrder.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoOrder.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoOrder.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
