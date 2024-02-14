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
    public class TestsController : Controller
    {
        private readonly TRDbContext _context;

        //Initializing controller
        public TestsController(TRDbContext context)
        {
            _context = context;
        }

        // GET: Tests
        //Fetches a specific test record by id
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tests.ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(m => m.TestID == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // GET: Tests/Create
        //Create with no params handles get request for /Test/Create and returns Create view
        //for creating a new test
        public IActionResult Create()
        {
            return View();
        }



        // POST: Tests/Create
        //Handles post request when form is submitted
        //Adds new Test record to DB and redicts to Index action if model state valid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TestID,TestName,TestDescription")] Test test)
        {
            if (ModelState.IsValid)
            {
                _context.Add(test);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test);
        }

        // GET: Tests/Edit/5
        //Handles get request for /Tests/Edit/{id} 
        //Fetches Test record for editing, returns the Edit view
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }

        // POST: Tests/Edit/5
        //Handles POST request for updating a Test record
        //Saves the changes to the DB and redirects to the Index action if model state valid
        //and the id mathces the TestID
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestID,TestName,TestDescription")] Test test)
        {
            if (id != test.TestID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExists(test.TestID))
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
            return View(test);
        }

        // GET: Tests/Delete/5
        //Handles GET request for /Tests/Delete/{id}
        //Retrieves Test Record for deletion, returns Delete view
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(m => m.TestID == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        //Handles POST request for confirming deletion, removes the Test record and 
        //redirects to Index action
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Checks if a Test record exists in DB with given ID
        private bool TestExists(int id)
        {
            return _context.Tests.Any(e => e.TestID == id);
        }
    }
}
