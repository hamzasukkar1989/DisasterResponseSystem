using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DisasterResponseSystem.Data;
using DisasterResponseSystem.Models;
using static DisasterResponseSystem.Common.Enums;

namespace DisasterResponseSystem.Controllers
{
    public class AffectedIndividualsRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AffectedIndividualsRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AffectedIndividualsRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.AffectedIndividualsRequests.Include(x=>x.AffectedIndividualsInkinds).ToListAsync());
        }

        // GET: AffectedIndividualsRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectedIndividualsRequests = await _context.AffectedIndividualsRequests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (affectedIndividualsRequests == null)
            {
                return NotFound();
            }

            return View(affectedIndividualsRequests);
        }

        // GET: AffectedIndividualsRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AffectedIndividualsRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(AffectedIndividualsRequests affectedIndividualsRequests)
        {
            if (ModelState.IsValid)
            {

                DateTime dateTime = DateTime.Now;
                affectedIndividualsRequests.RequestDate = dateTime;



                affectedIndividualsRequests.EvaluatesRequestsStatus = EvaluatesRequestsStatus.Pending;

                double evaluation = 0;

                if (affectedIndividualsRequests.AffectedType == AffectedType.Earthquake)
                    evaluation += 4;
                else if (affectedIndividualsRequests.AffectedType == AffectedType.Emigration)
                    evaluation += 3;
                else if (affectedIndividualsRequests.AffectedType == AffectedType.Fire)
                    evaluation += 2;
                else if (affectedIndividualsRequests.AffectedType == AffectedType.Theft)
                    evaluation += 1;

                if (affectedIndividualsRequests.MaritalStatus == MaritalStatus.Married)
                    evaluation += 1;

                if (affectedIndividualsRequests.FamilyMembers > 2)
                    evaluation += affectedIndividualsRequests.FamilyMembers / 2;

                affectedIndividualsRequests.Evaluation = evaluation;
                _context.Add(affectedIndividualsRequests);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(affectedIndividualsRequests);
        }

        // GET: AffectedIndividualsRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectedIndividualsRequests = await _context.AffectedIndividualsRequests.FindAsync(id);
            if (affectedIndividualsRequests == null)
            {
                return NotFound();
            }
            return View(affectedIndividualsRequests);
        }

        // POST: AffectedIndividualsRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,City,Country,Phone,FamilyMembers,MaritalStatus,Evaluation,RequestDate,AffectedType,EvaluatesRequestsStatus")] AffectedIndividualsRequests affectedIndividualsRequests)
        {
            if (id != affectedIndividualsRequests.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(affectedIndividualsRequests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AffectedIndividualsRequestsExists(affectedIndividualsRequests.ID))
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
            return View(affectedIndividualsRequests);
        }

        // GET: AffectedIndividualsRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var affectedIndividualsRequests = await _context.AffectedIndividualsRequests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (affectedIndividualsRequests == null)
            {
                return NotFound();
            }

            return View(affectedIndividualsRequests);
        }

        // POST: AffectedIndividualsRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var affectedIndividualsRequests = await _context.AffectedIndividualsRequests.FindAsync(id);
            _context.AffectedIndividualsRequests.Remove(affectedIndividualsRequests);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Recived(int? id)
        {
  
            var affectedIndividualsRequests = await _context.AffectedIndividualsRequests
                .FirstOrDefaultAsync(m => m.ID == id);
            affectedIndividualsRequests.EvaluatesRequestsStatus = EvaluatesRequestsStatus.Done;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AffectedIndividualsRequestsExists(int id)
        {
            return _context.AffectedIndividualsRequests.Any(e => e.ID == id);
        }
    }
}
