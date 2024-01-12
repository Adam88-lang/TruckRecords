﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckRecords.Models;

namespace TruckRecords.Controllers
{
    public class TestDataController : Controller
    {
        private readonly TRDbContext _context;
        public TestDataController(TRDbContext context)
        {
            _context = context;
        }

        //custom method for adding test data from input form
        [HttpPost]
        public async Task<IActionResult> SubmitData(TestResult testResults
            )
        {
            if (ModelState.IsValid)
            {
                _context.TestResults.Add(testResults);
                await _context.SaveChangesAsync();
                return RedirectToAction("SuccessPage"); // Or any other view you want to redirect to
            }

            return View("YourFormView", testResults); // Return to the form view if validation fails
        }
        // GET: TestDataController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TestDataController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestDataController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestDataController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestDataController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestDataController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestDataController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestDataController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}