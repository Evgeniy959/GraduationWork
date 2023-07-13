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
    public class RoomTariffAdminController : Controller
    {
        private readonly AppDbContext _context;

        public RoomTariffAdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RoomTariffAdmin
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TariffAdmins.Include(r => r.Category).Include(r => r.TariffPlan);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RoomTariffAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TariffAdmins == null)
            {
                return NotFound();
            }

            var roomTariffAdmin = await _context.TariffAdmins
                .Include(r => r.Category)
                .Include(r => r.TariffPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTariffAdmin == null)
            {
                return NotFound();
            }

            return View(roomTariffAdmin);
        }

        // GET: RoomTariffAdmin/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name");
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id");
            return View();
        }

        // POST: RoomTariffAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryId,TariffPlanId,Price")] RoomTariffAdmin roomTariffAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomTariffAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", roomTariffAdmin.CategoryId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id", roomTariffAdmin.TariffPlanId);
            return View(roomTariffAdmin);
        }

        // GET: RoomTariffAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TariffAdmins == null)
            {
                return NotFound();
            }

            var roomTariffAdmin = await _context.TariffAdmins.FindAsync(id);
            if (roomTariffAdmin == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", roomTariffAdmin.CategoryId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id", roomTariffAdmin.TariffPlanId);
            return View(roomTariffAdmin);
        }

        // POST: RoomTariffAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoryId,TariffPlanId,Price")] RoomTariffAdmin roomTariffAdmin)
        {
            if (id != roomTariffAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomTariffAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomTariffAdminExists(roomTariffAdmin.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categorys, "Id", "Name", roomTariffAdmin.CategoryId);
            ViewData["TariffPlanId"] = new SelectList(_context.TariffPlans, "Id", "Id", roomTariffAdmin.TariffPlanId);
            return View(roomTariffAdmin);
        }

        // GET: RoomTariffAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TariffAdmins == null)
            {
                return NotFound();
            }

            var roomTariffAdmin = await _context.TariffAdmins
                .Include(r => r.Category)
                .Include(r => r.TariffPlan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomTariffAdmin == null)
            {
                return NotFound();
            }

            return View(roomTariffAdmin);
        }

        // POST: RoomTariffAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TariffAdmins == null)
            {
                return Problem("Entity set 'AppDbContext.TariffAdmins'  is null.");
            }
            var roomTariffAdmin = await _context.TariffAdmins.FindAsync(id);
            if (roomTariffAdmin != null)
            {
                _context.TariffAdmins.Remove(roomTariffAdmin);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomTariffAdminExists(int id)
        {
          return (_context.TariffAdmins?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
