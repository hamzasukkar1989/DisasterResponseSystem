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
    public class inkindAidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public inkindAidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: inkindAids
        public async Task<IActionResult> Index()
        {
            return View(await _context.inkindAids.Include(x=>x.DonorEntitie).ToListAsync());
        }

        // GET: inkindAids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inkindAids = await _context.inkindAids
                .FirstOrDefaultAsync(m => m.inkindAidsID == id);
            if (inkindAids == null)
            {
                return NotFound();
            }

            return View(inkindAids);
        }

        // GET: inkindAids/Create
        public IActionResult Create()
        {
            ViewBag.DonorEntities = new SelectList(_context.DonorEntities, "DonorEntitiesID", "Name");
            ViewBag.InkindTypes = new SelectList(_context.InkindTypes, "ID", "InkindName");
            return View();
        }

        // POST: inkindAids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(inkindAids inkindAids, int DonorEntitiesID,int InkindTypeID)
        {
            if (ModelState.IsValid)
            {
                var DonorEntitie = _context.DonorEntities.Where(x => x.DonorEntitiesID == DonorEntitiesID).FirstOrDefault();
                var inkindType = _context.InkindTypes.Where(x => x.ID == InkindTypeID).FirstOrDefault();
                inkindType.QTY = inkindAids.QTY;
                inkindAids.DonorEntitie = DonorEntitie;
                _context.Add(inkindAids);
                _context.Update(inkindType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inkindAids);
        }

        // GET: inkindAids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inkindAids = await _context.inkindAids.FindAsync(id);
            if (inkindAids == null)
            {
                return NotFound();
            }
            return View(inkindAids);
        }

        // POST: inkindAids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("inkindAidsID,QTY")] inkindAids inkindAids)
        {
            if (id != inkindAids.inkindAidsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inkindAids);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!inkindAidsExists(inkindAids.inkindAidsID))
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
            return View(inkindAids);
        }

        // GET: inkindAids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inkindAids = await _context.inkindAids
                .FirstOrDefaultAsync(m => m.inkindAidsID == id);
            if (inkindAids == null)
            {
                return NotFound();
            }

            return View(inkindAids);
        }

        // POST: inkindAids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inkindAids = await _context.inkindAids.FindAsync(id);
            _context.inkindAids.Remove(inkindAids);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool inkindAidsExists(int id)
        {
            return _context.inkindAids.Any(e => e.inkindAidsID == id);
        }
    }
}
