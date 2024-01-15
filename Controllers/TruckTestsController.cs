using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckRecords.Models;

namespace TruckRecords.Controllers
{
    public class TruckTestsController : Controller
    {
        private readonly TRDbContext _context;

        public TruckTestsController(TRDbContext context)
        {
            _context = context;
        }

        // GET: TruckTests
        public async Task<IActionResult> Index()
        {
            return View(await _context.TruckTest.ToListAsync());
        }

        // GET: TruckTests/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckTest = await _context.TruckTest
                .FirstOrDefaultAsync(m => m.TruckName == id);
            if (truckTest == null)
            {
                return NotFound();
            }

            return View(truckTest);
        }

        private void PopulateTrucksDropDownList(object selectedTruck = null)
        {
            var trucksQuery = from t in new List<string> { "Peterbilt Model 389", "DAF LF45", "Kenworth T660" }
                              select new { Value = t, Text = t };

            ViewBag.TrucksList = new SelectList(trucksQuery, "Value", "Text", selectedTruck);
        }



        // GET: TruckTests/Create
        public IActionResult Create()
        {
            PopulateTrucksDropDownList();
            return View();
        }

        // POST: TruckTests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckName,ComponentTested,TestType,TestDate,Score")] TruckTest truckTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truckTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truckTest);
        }

        // GET: TruckTests/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckTest = await _context.TruckTest.FindAsync(id);
            if (truckTest == null)
            {
                return NotFound();
            }

            PopulateTrucksDropDownList(truckTest.TruckName);
            return View(truckTest);
        }

        // POST: TruckTests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TruckName,ComponentTested,TestType,TestDate,Score")] TruckTest truckTest)
        {
            if (id != truckTest.TruckName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truckTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckTestExists(truckTest.TruckName))
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
            return View(truckTest);
        }

        // GET: TruckTests/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truckTest = await _context.TruckTest
                .FirstOrDefaultAsync(m => m.TruckName == id);
            if (truckTest == null)
            {
                return NotFound();
            }

            return View(truckTest);
        }

        // POST: TruckTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var truckTest = await _context.TruckTest.FindAsync(id);
            if (truckTest != null)
            {
                _context.TruckTest.Remove(truckTest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckTestExists(string id)
        {
            return _context.TruckTest.Any(e => e.TruckName == id);
        }
    }
}
