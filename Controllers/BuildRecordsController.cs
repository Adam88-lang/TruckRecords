using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckRecords.Models;
using TestResult = TruckRecords.Models.TestResult;


namespace TruckRecords.Controllers
{
    public class BuildRecordsController : Controller
    {
        private readonly TRDbContext _context;

        public BuildRecordsController(TRDbContext context)
        {
            _context = context;
        }

        // GET: BuildRecords
        public async Task<IActionResult> Index()
        {
            var tRDbContext = _context.BuildRecords.Include(b => b.Component).Include(b => b.Truck);
            return View(await tRDbContext.ToListAsync());
        }

        // GET: BuildRecords/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildRecord = await _context.BuildRecords
                .Include(b => b.Component)
                .Include(b => b.Truck)
                .FirstOrDefaultAsync(m => m.BuildRecordID == id);
            if (buildRecord == null)
            {
                return NotFound();
            }

            return View(buildRecord);
        }

        // GET: BuildRecords/Create
        public IActionResult Create()
        {
            ViewData["ComponentID"] = new SelectList(_context.Components, "ComponentID", "ComponentID");
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID");
            return View();
        }

        // POST: BuildRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuildRecordID,TruckID,ComponentID,BuildDate,Notes")] BuildRecord buildRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buildRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComponentID"] = new SelectList(_context.Components, "ComponentID", "ComponentID", buildRecord.ComponentID);
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID", buildRecord.TruckID);
            return View(buildRecord);
        }

        // GET: BuildRecords/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildRecord = await _context.BuildRecords.FindAsync(id);
            if (buildRecord == null)
            {
                return NotFound();
            }
            ViewData["ComponentID"] = new SelectList(_context.Components, "ComponentID", "ComponentID", buildRecord.ComponentID);
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID", buildRecord.TruckID);
            return View(buildRecord);
        }

        // POST: BuildRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuildRecordID,TruckID,ComponentID,BuildDate,Notes")] BuildRecord buildRecord)
        {
            if (id != buildRecord.BuildRecordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buildRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildRecordExists(buildRecord.BuildRecordID))
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
            ViewData["ComponentID"] = new SelectList(_context.Components, "ComponentID", "ComponentID", buildRecord.ComponentID);
            ViewData["TruckID"] = new SelectList(_context.Trucks, "TruckID", "TruckID", buildRecord.TruckID);
            return View(buildRecord);
        }

        // GET: BuildRecords/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buildRecord = await _context.BuildRecords
                .Include(b => b.Component)
                .Include(b => b.Truck)
                .FirstOrDefaultAsync(m => m.BuildRecordID == id);
            if (buildRecord == null)
            {
                return NotFound();
            }

            return View(buildRecord);
        }

        // POST: BuildRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buildRecord = await _context.BuildRecords.FindAsync(id);
            if (buildRecord != null)
            {
                _context.BuildRecords.Remove(buildRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildRecordExists(int id)
        {
            return _context.BuildRecords.Any(e => e.BuildRecordID == id);
        }
    }
}
