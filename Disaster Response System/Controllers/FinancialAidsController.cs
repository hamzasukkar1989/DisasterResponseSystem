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
    public class FinancialAidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FinancialAidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FinancialAids
        public async Task<IActionResult> Index()
        {
            return View(await _context.FinancialAids.Include(x=>x.DonorEntitie).ToListAsync());
        }

        // GET: FinancialAids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAids = await _context.FinancialAids
                .FirstOrDefaultAsync(m => m.FinancialAidsID == id);
            if (financialAids == null)
            {
                return NotFound();
            }

            return View(financialAids);
        }

        // GET: FinancialAids/Create
        public IActionResult Create()
        {
            var DonorEntities = _context.DonorEntities.ToList();
            ViewBag.DonorEntities = new SelectList(_context.DonorEntities, "DonorEntitiesID", "Name");
            return View();
        }

        // POST: FinancialAids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(FinancialAids financialAids,int DonorEntitiesID)
        {
            if (ModelState.IsValid)
            {
                var DonorEntitie = _context.DonorEntities.Where(x => x.DonorEntitiesID == DonorEntitiesID).FirstOrDefault();
                financialAids.DonorEntitie = DonorEntitie;
                _context.Add(financialAids);

                var TotalFinancialAids = _context.TotalFinancialAids.FirstOrDefault();
                if (TotalFinancialAids == null)
                {
                    TotalFinancialAids totalFinancialAids = new TotalFinancialAids();
                    totalFinancialAids.Amount = financialAids.Amount;
                    _context.Add(totalFinancialAids);
                }
                else
                {
                    TotalFinancialAids.Amount+= financialAids.Amount;
                    _context.Update(TotalFinancialAids);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(financialAids);
        }

        // GET: FinancialAids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAids = await _context.FinancialAids.FindAsync(id);
            if (financialAids == null)
            {
                return NotFound();
            }
            return View(financialAids);
        }

        // POST: FinancialAids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FinancialAidsID,Amount")] FinancialAids financialAids)
        {
            if (id != financialAids.FinancialAidsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(financialAids);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FinancialAidsExists(financialAids.FinancialAidsID))
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
            return View(financialAids);
        }

        // GET: FinancialAids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var financialAids = await _context.FinancialAids
                .FirstOrDefaultAsync(m => m.FinancialAidsID == id);
            if (financialAids == null)
            {
                return NotFound();
            }

            return View(financialAids);
        }

        // POST: FinancialAids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var financialAids = await _context.FinancialAids.FindAsync(id);
            _context.FinancialAids.Remove(financialAids);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FinancialAidsExists(int id)
        {
            return _context.FinancialAids.Any(e => e.FinancialAidsID == id);
        }
    }
}
