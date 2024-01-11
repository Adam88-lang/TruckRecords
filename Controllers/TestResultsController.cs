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
    public class TestResultsController : Controller
    {
        private readonly TRDbContext _context;

        public TestResultsController(TRDbContext context)
        {
            _context = context;
        }

        // GET: TestResults
        public async Task<IActionResult> Index()
        {
            var tRDbContext = _context.TestResults.Include(t => t.Test).Include(t => t.Truck);
            return View(await tRDbContext.ToListAsync());
        }

        // GET: TestResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults
                .Include(t => t.Test)
                .Include(t => t.Truck)
                .FirstOrDefaultAsync(m => m.TestResultID == id);
            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }

        // GET: TestResults/Create
        public IActionResult Create()
        {
            ViewData["TestID"] = new SelectList(_context.Tests, "TestID", "TestID");
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID");
            return View();
        }

        // POST: TestResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestResultID,TruckID,TestID,DateConducted,Result,Comments")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TestID"] = new SelectList(_context.Tests, "TestID", "TestID", testResult.TestID);
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID", testResult.TruckID);
            return View(testResult);
        }

        // GET: TestResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult == null)
            {
                return NotFound();
            }
            ViewData["TestID"] = new SelectList(_context.Tests, "TestID", "TestID", testResult.TestID);
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID", testResult.TruckID);
            return View(testResult);
        }

        // POST: TestResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestResultID,TruckID,TestID,DateConducted,Result,Comments")] TestResult testResult)
        {
            if (id != testResult.TestResultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestResultExists(testResult.TestResultID))
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
            ViewData["TestID"] = new SelectList(_context.Tests, "TestID", "TestID", testResult.TestID);
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID", testResult.TruckID);
            return View(testResult);
        }

        // GET: TestResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults
                .Include(t => t.Test)
                .Include(t => t.Truck)
                .FirstOrDefaultAsync(m => m.TestResultID == id);
            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }

        // POST: TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult != null)
            {
                _context.TestResults.Remove(testResult);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestResultExists(int id)
        {
            return _context.TestResults.Any(e => e.TestResultID == id);
        }
    }
}
