using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisasterResponseSystem.Data;
using DisasterResponseSystem.Models;

namespace DisasterResponseSystem.Controllers
{
    public class DonorEntitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DonorEntitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DonorEntities
        public async Task<IActionResult> Index()
        {
            return View(await _context.DonorEntities.ToListAsync());
        }

        // GET: DonorEntities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorEntities = await _context.DonorEntities
                .FirstOrDefaultAsync(m => m.DonorEntitiesID == id);
            if (donorEntities == null)
            {
                return NotFound();
            }

            return View(donorEntities);
        }

        // GET: DonorEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonorEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([Bind("DonorEntitiesID,Name")] DonorEntities donorEntities)
        {
            if (ModelState.IsValid)
            {     
                _context.Add(donorEntities);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(donorEntities);
        }

        // GET: DonorEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorEntities = await _context.DonorEntities.FindAsync(id);
            if (donorEntities == null)
            {
                return NotFound();
            }
            return View(donorEntities);
        }

        // POST: DonorEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonorEntitiesID,Name")] DonorEntities donorEntities)
        {
            if (id != donorEntities.DonorEntitiesID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donorEntities);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorEntitiesExists(donorEntities.DonorEntitiesID))
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
            return View(donorEntities);
        }

        // GET: DonorEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donorEntities = await _context.DonorEntities
                .FirstOrDefaultAsync(m => m.DonorEntitiesID == id);
            if (donorEntities == null)
            {
                return NotFound();
            }

            return View(donorEntities);
        }

        // POST: DonorEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donorEntities = await _context.DonorEntities.FindAsync(id);
            _context.DonorEntities.Remove(donorEntities);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonorEntitiesExists(int id)
        {
            return _context.DonorEntities.Any(e => e.DonorEntitiesID == id);
        }
    }
}
