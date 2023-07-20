using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelWebsiteBooking.Models;
using HotelWebsiteBooking.Models.Entity;

namespace HotelWebsiteBooking.Controllers
{
    public class RoomTariffController : Controller
    {
        private readonly AppDbContext _context;

        public RoomTariffController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomTariff
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tariffs.Include(r => r.Room).Include(r => r.TariffPlan);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RoomTariff/Details/5
        public async Task<IActionResult> Tariff(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var roomTariff = await _context.Tariffs
                .Include(r => r.Room)
                .Include(r => r.TariffPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTariff == null)
            {
                return NotFound();
            }

            return View(roomTariff);
        }

        // GET: RoomTariff/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id");
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id");
            return View();
        }

        // POST: RoomTariff/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,TariffPlanId,Price")] RoomTariff roomTariff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomTariff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomTariff.RoomId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id", roomTariff.TariffPlanId);
            return View(roomTariff);
        }

        // GET: RoomTariff/Edit/5
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomTariff.RoomId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id", roomTariff.TariffPlanId);
            return View(roomTariff);
        }

        // POST: RoomTariff/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,TariffPlanId,Price")] RoomTariff roomTariff)
        {
            if (id != roomTariff.Id)
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
                    if (!RoomTariffExists(roomTariff.Id))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "Id", "Id", roomTariff.RoomId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id", roomTariff.TariffPlanId);
            return View(roomTariff);
        }

        // GET: RoomTariff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tariffs == null)
            {
                return NotFound();
            }

            var roomTariff = await _context.Tariffs
                .Include(r => r.Room)
                .Include(r => r.TariffPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTariff == null)
            {
                return NotFound();
            }

            return View(roomTariff);
        }

        // POST: RoomTariff/Delete/5
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
          return (_context.Tariffs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
