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
    public class InkindTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InkindTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InkindTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.InkindTypes.ToListAsync());
        }

        // GET: InkindTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inkindTypes = await _context.InkindTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inkindTypes == null)
            {
                return NotFound();
            }

            return View(inkindTypes);
        }

        // GET: InkindTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InkindTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,InkindName,QTY")] InkindTypes inkindTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inkindTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inkindTypes);
        }

        // GET: InkindTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inkindTypes = await _context.InkindTypes.FindAsync(id);
            if (inkindTypes == null)
            {
                return NotFound();
            }
            return View(inkindTypes);
        }

        // POST: InkindTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,InkindName,QTY")] InkindTypes inkindTypes)
        {
            if (id != inkindTypes.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inkindTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InkindTypesExists(inkindTypes.ID))
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
            return View(inkindTypes);
        }

        // GET: InkindTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inkindTypes = await _context.InkindTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inkindTypes == null)
            {
                return NotFound();
            }

            return View(inkindTypes);
        }

        // POST: InkindTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inkindTypes = await _context.InkindTypes.FindAsync(id);
            _context.InkindTypes.Remove(inkindTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InkindTypesExists(int id)
        {
            return _context.InkindTypes.Any(e => e.ID == id);
        }
    }
}
