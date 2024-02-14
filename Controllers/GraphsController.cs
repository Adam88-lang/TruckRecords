using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TruckRecords.Models;
using TestResult = TruckRecords.Models.TestResult;


namespace TruckRecords.Controllers
{
    public class GraphsController : Controller
    {
        private readonly TRDbContext _context;

        public GraphsController(TRDbContext context)
        {
            _context = context;
        }

        public IActionResult TruckSelect()
        {
            return View();
        }

        public IActionResult TestTypes389()
        {
            return View();
        }

        public ActionResult Dynamometer389()
        {
            var truckTests = _context.TruckTests
                .Where(t => t.TestType == "Dynamometer Test" && t.TruckName == "Peterbilt Model 389") // Filter for dynamometer tests
                .ToList();

            var truckScores = truckTests
                .GroupBy(t => t.TruckName)
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

            ViewBag.TruckScoresJson = Newtonsoft.Json.JsonConvert.SerializeObject(truckScores);
            ViewBag.TruckScores = truckScores;
            return View("~/Views/Graphs/Dynamometer389.cshtml");

        }

        public ActionResult Durability389()
        {
            var truckTests = _context.TruckTests
                .Where(t => t.TestType == "Durability Test" && t.TruckName == "Peterbilt Model 389") // Filter for dynamometer tests
                .ToList();

            var truckScores = truckTests
                .GroupBy(t => t.TruckName)
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

            ViewBag.TruckScoresJson = Newtonsoft.Json.JsonConvert.SerializeObject(truckScores);
            ViewBag.TruckScores = truckScores;
            return View("~/Views/Graphs/Durability389.cshtml");

        }

        public ActionResult Emissions389()
        {
            var truckTests = _context.TruckTests
                .Where(t => t.TestType == "Emissions Test" && t.TruckName == "Peterbilt Model 389") // Filter for dynamometer tests
                .ToList();

            var truckScores = truckTests
                .GroupBy(t => t.TruckName)
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

            ViewBag.TruckScoresJson = Newtonsoft.Json.JsonConvert.SerializeObject(truckScores);
            ViewBag.TruckScores = truckScores;
            return View("~/Views/Graphs/Emissions389.cshtml");

        }



    }
}
