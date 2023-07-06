using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HotelAdmin.Helpers;
using HotelAdmin.Service.RoomService;

namespace HotelAdmin.Controllers
{
    public class RoomController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IDaoRoom _daoRoom;

        public RoomController(AppDbContext context, IDaoRoom daoRoom)
        {
            _context = context;
            _daoRoom = daoRoom;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.TotalPages = Math.Ceiling((await _context.Rooms.ToListAsync()).Count / 10.0);
            ViewBag.CurrentPage = page;

            return View(_daoRoom.IndexAsync(page).Result);
        }

        public IActionResult Details(int id)
        {
            if (_daoRoom.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoRoom.GetAsync(id).Result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id,Number,Photo,Сategory,Price,PersonsCount")] Room room, RoomDate date, IFormFile photo)
        {
            if (ModelState.IsValid && _daoRoom.AddAsync(room, date, photo).Result == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Status"] = "Room exsist!";
                return View(room);
            }
            
        }

        public IActionResult Edit(int id)
        {
            if (_daoRoom.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoRoom.GetAsync(id).Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,Number,Photo,Сategory,Price,PersonsCount")] Room room, IFormFile photo)
        {
            if (ModelState.IsValid && _daoRoom.UpdateAsync(room, photo).Result == true)
            {
                return RedirectToAction("Index");
            }
            return View(room);

        }

        public IActionResult Delete(int id)
        {
            if (_daoRoom.GetAsync(id).Result == null)
            {
                return NotFound();
            }

            return View(_daoRoom.GetAsync(id).Result);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _daoRoom.DeleteConfirmedAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
