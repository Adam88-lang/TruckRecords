using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TruckRecords.Models;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using TestResult = TruckRecords.Models.TestResult;


namespace TruckRecords.Controllers
{
    public class TruckTestsController : Controller
    {
        private readonly TRDbContext _context;

        public TruckTestsController(TRDbContext context)
        {
            _context = context;
        }

        public ActionResult TruckScoreGraph()
        {

            //making a list out of the truckTest table
            var truckTests = _context.TruckTests.ToList();

            var truckScores = truckTests
                //groupe by truck name
                .GroupBy(t => t.TruckName)

                //each group has a truck name, a test, and an id
                .Select((group, index) => new
                {
                    TruckName = group.Key,
                    Tests = group.OrderBy(t => t.TestDate)
                    .Select(t => new
                    {
                        TestDate = t.TestDate.ToString("yyyy-MM-dd"),
                        t.Score
                    }).ToList(),
                    Id = "truck_" + index // Generate a unique ID for each group
                }).ToList();

            //making a json object out of the truckScores list for javascript
            ViewBag.TruckScoresJson = Newtonsoft.Json.JsonConvert.SerializeObject(truckScores);
            ViewBag.TruckScores = truckScores;
            //returns default view
            return View();
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
       


        [HttpGet]
        public JsonResult GetTestTypesByComponent(string component)
        {
            // Retrieve test types based on the selected component
            
            var testTypes = new List<SelectListItem>();

            if (component == "Engine")
            {
                testTypes.Add(new SelectListItem { Value = "Dynamometer Test", Text = "Dynamometer Test" });
                testTypes.Add(new SelectListItem { Value = "Durability Test", Text = "Durability Test" });
                testTypes.Add(new SelectListItem { Value = "Emissions Test", Text = "Emissions Test" });
            }
            else if (component == "Brakes")
            {
                testTypes.Add(new SelectListItem { Value = "Brake Fade Test", Text = "Brake Fade Test" });
                testTypes.Add(new SelectListItem { Value = "Brake Performance Test", Text = "Brake Performance Test" });
                testTypes.Add(new SelectListItem { Value = "Brake Test", Text = "Brake Test" });
            }
            else if (component == "Suspension")
            {
                testTypes.Add(new SelectListItem { Value = "Bump Steer Test", Text = "Bump Steer Test" });
                testTypes.Add(new SelectListItem { Value = "Durability Test", Text = "Durability Test" });
                testTypes.Add(new SelectListItem { Value = "Ride Quality Test", Text = "Ride Quality Test" });
            }
            else if (component == "Electrical System")
            {
                testTypes.Add(new SelectListItem { Value = "Battery Test", Text = "Battery Test" });
                testTypes.Add(new SelectListItem { Value = "Charging System Test", Text = "Charging System Test" });
                testTypes.Add(new SelectListItem { Value = "Starter Test", Text = "Starter Test" });
            }
            else if (component == "Computer")
            {
                testTypes.Add(new SelectListItem { Value = "Diagnostics Check", Text = "Diagnostics Check" });
                testTypes.Add(new SelectListItem { Value = "Software Validation", Text = "Software Validation" });
                testTypes.Add(new SelectListItem { Value = "Environmental Stress Testing", Text = "Environmental Stress Testing" });
            }
            
            // Return the test types as a JSON object
            return Json(testTypes);
        }


        private void PopulateTrucksDropDownList(object selectedTruck = null)
        {
            var trucksQuery = from t in new List<string> { "Peterbilt Model 389", "DAF LF45", "Kenworth T660" }
                              select new { Value = t, Text = t };

            ViewBag.TrucksList = new SelectList(trucksQuery, "Value", "Text", selectedTruck);
        }

        private void PopulateComponentTestedDropDownList(object selectedComponent = null)
        {
            var components = new List<string> { "Engine", "Brakes", "Suspension", "Electrical System", "Computer" };

            ViewBag.ComponentTested = components.Select(x => new SelectListItem
            {
                Value = x,
                Text = x
            }).ToList();
        }


        // GET: TruckTests/Create
        public IActionResult Create()
        {
            PopulateTrucksDropDownList();
            PopulateComponentTestedDropDownList(); // Add this line
            return View();
        }

        // POST: TruckTests/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TruckName,ComponentTested,TestType,TestDate,Score")] TruckTest truckTest)
        {
            if (ModelState.IsValid)
            {
                // Set time component of TestDate to midnight
                truckTest.TestDate = truckTest.TestDate.Date;

                _context.Add(truckTest);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Record Created Successfully";
                return RedirectToAction(nameof(Create));
            }
            // Collecting the validation errors
            var validationErrors = ModelState
                                   .Where(ms => ms.Value.Errors.Any())
                                   .SelectMany(ms => ms.Value.Errors)
                                   .Select(e => e.ErrorMessage)
                                   .ToList();

            // Passing the errors to the view
            ViewBag.ValidationErrors = validationErrors;
            PopulateTrucksDropDownList(truckTest.TruckName);
            PopulateComponentTestedDropDownList(truckTest.ComponentTested); //this fixed it breaking on submit
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