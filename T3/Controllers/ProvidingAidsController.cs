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
    public class ProvidingAidsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProvidingAidsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProvidingAids
        public async Task<IActionResult> Index()
        {
            var ProvidingAids = new ProvidingAids();
            var AffectedIndividualsRequests = _context.AffectedIndividualsRequests.Where(x => x.EvaluatesRequestsStatus == EvaluatesRequestsStatus.Pending).OrderByDescending(x => x.Evaluation).ToList();
            if(AffectedIndividualsRequests.Count>0)
                ProvidingAids.AffectedIndividualsRequests = AffectedIndividualsRequests;


            return View(ProvidingAids);
        }

        // GET: ProvidingAids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providingAids = await _context.ProvidingAids
                .FirstOrDefaultAsync(m => m.ID == id);
            if (providingAids == null)
            {
                return NotFound();
            }

            return View(providingAids);
        }

        // GET: ProvidingAids/Create
        public IActionResult Create()
        {
            return View();
        }



        public IActionResult DisbursesAid()
        {
            var TotalFinancialAids = _context.TotalFinancialAids.FirstOrDefault();
            var InkindTypes = _context.InkindTypes.ToList();

            //Disburses Financial Aid
            //Disburses Financial According to priority each Individual take 5% from The remaining balance

            if (TotalFinancialAids != null || InkindTypes != null)
            {
                var AffectedIndividualsRequests = _context.AffectedIndividualsRequests.OrderByDescending(x => x.Evaluation).Where(x => x.EvaluatesRequestsStatus == EvaluatesRequestsStatus.Pending).ToList();
                foreach (var affectedIndividualsRequests in AffectedIndividualsRequests)
                {
                    if (TotalFinancialAids != null && TotalFinancialAids.Amount > 0)
                    {
                        var Amountspent = TotalFinancialAids.Amount * 5 / 100;
                        TotalFinancialAids.Amount -= Amountspent;
                        affectedIndividualsRequests.AmountSpent = Amountspent;
                        affectedIndividualsRequests.EvaluatesRequestsStatus = EvaluatesRequestsStatus.ForDelivery;
                        _context.Update(affectedIndividualsRequests);
                        _context.Update(TotalFinancialAids);

                    }

                    //Disburses Inkind Aid
                    //Disburses Financial According to priority each Individual take 5% from The remaining inkind Types
                    if (InkindTypes != null)
                    {
                        List<AffectedIndividualsInkind> AffectedIndividualsInkinds = new List<AffectedIndividualsInkind>();

                        //This to check there are items to Adis
                        bool AllInkindQtyisempty = true;

                        foreach (var InkindType in InkindTypes)
                        {

                            if (InkindType.QTY > 0)
                            {


                                var AffectedIndividualsInkind = new AffectedIndividualsInkind();
                                AffectedIndividualsInkind.Name = InkindType.InkindName;
                                AffectedIndividualsInkind.QTY = 1;

                                AffectedIndividualsInkinds.Add(AffectedIndividualsInkind);



                                InkindType.QTY -= 1;

                                _context.Update(InkindType);

                                AllInkindQtyisempty = false;
                            }

                        }
                        if (!AllInkindQtyisempty)
                        {
                            affectedIndividualsRequests.AffectedIndividualsInkinds = AffectedIndividualsInkinds;
                            _context.Update(affectedIndividualsRequests);
                            affectedIndividualsRequests.EvaluatesRequestsStatus = EvaluatesRequestsStatus.ForDelivery;
                        }


                    }



                    _context.SaveChanges();

                }


             
            }

            var ProvidingAids = new ProvidingAids();
            var AffectedIndividualsRequests2 = _context.AffectedIndividualsRequests.Where(x => x.EvaluatesRequestsStatus == EvaluatesRequestsStatus.Pending).OrderByDescending(x => x.Evaluation).ToList();
            if (AffectedIndividualsRequests2.Count > 0)
                ProvidingAids.AffectedIndividualsRequests = AffectedIndividualsRequests2;


            return View("index", ProvidingAids);
        }
        // GET: ProvidingAids/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providingAids = await _context.ProvidingAids.FindAsync(id);
            if (providingAids == null)
            {
                return NotFound();
            }
            return View(providingAids);
        }

        // POST: ProvidingAids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID")] ProvidingAids providingAids)
        {
            if (id != providingAids.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(providingAids);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvidingAidsExists(providingAids.ID))
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
            return View(providingAids);
        }

        // GET: ProvidingAids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providingAids = await _context.ProvidingAids
                .FirstOrDefaultAsync(m => m.ID == id);
            if (providingAids == null)
            {
                return NotFound();
            }

            return View(providingAids);
        }

        // POST: ProvidingAids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var providingAids = await _context.ProvidingAids.FindAsync(id);
            _context.ProvidingAids.Remove(providingAids);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvidingAidsExists(int id)
        {
            return _context.ProvidingAids.Any(e => e.ID == id);
        }
    }
}
