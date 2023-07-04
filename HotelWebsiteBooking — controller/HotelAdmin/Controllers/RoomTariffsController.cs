using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Models;
using HotelAdmin.Models.Entity;

namespace HotelAdmin.Controllers
{
    public class RoomTariffsController : Controller
    {
        private readonly AppDbContext _context;

        public RoomTariffsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomTariffs
        public async Task<IActionResult> Index()
        {
              return _context.Tariffs != null ? 
                          View(await _context.Tariffs.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Tariffs'  is null.");
        }

        // GET: RoomTariffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var roomTariff = await _context.Tariffs
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomTariff == null)
            {
                return NotFound();
            }

            return View(roomTariff);
        }

        // GET: RoomTariffs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoomTariffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoomId,Description,Price")] RoomTariff roomTariff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomTariff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomTariff);
        }

        // GET: RoomTariffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var roomTariff = await _context.Tariffs.FindAsync(id);
            if (roomTariff == null)
            {
                return NotFound();
            }
            return View(roomTariff);
        }

        // POST: RoomTariffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,Description,Price")] RoomTariff roomTariff)
        {
            if (id != roomTariff.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomTariff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTariffExists(roomTariff.RoomId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(roomTariff);
        }

        // GET: RoomTariffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var roomTariff = await _context.Tariffs
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (roomTariff == null)
            {
                return NotFound();
            }

            return View(roomTariff);
        }

        // POST: RoomTariffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tariffs == null)
            {
                return Problem("Entity set 'AppDbContext.Tariffs'  is null.");
            }
            var roomTariff = await _context.Tariffs.FindAsync(id);
            if (roomTariff != null)
            {
                _context.Tariffs.Remove(roomTariff);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTariffExists(int id)
        {
          return (_context.Tariffs?.Any(e => e.RoomId == id)).GetValueOrDefault();
        }
    }
}
