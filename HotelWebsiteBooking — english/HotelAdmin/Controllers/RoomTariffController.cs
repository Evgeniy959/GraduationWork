using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using HotelAdmin.Service.RoomService;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HotelAdmin.Service.RoomTariffService;

namespace HotelAdmin.Controllers
{
    public class RoomTariffController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoRoomTariff _daoRoomTariff;

        public RoomTariffController(AppDbContext context, IDaoRoomTariff daoRoomTariff)
        {
            _context = context;
            _daoRoomTariff = daoRoomTariff;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Tariffs.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoRoomTariff.IndexAsync(page).Result);
        }

    }
}
