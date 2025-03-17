using Microsoft.AspNetCore.Mvc;
using MyApp.Data;
using MyApp.Models;
using System.Diagnostics;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

public class HomeController : Controller
{
    private readonly MyAppContext _db;

    public HomeController(MyAppContext db)
    {
        _db = db;
    }

    // Your existing action methods...

    public IActionResult ProductCategoryChart()
    {
        // Get data from your database
        var categories = _db.Categories.ToList();
        var products = _db.Items.ToList();

        // Count products in each category
        var categoryProductCount = categories.Select(c => new {
            CategoryName = c.Name,
            ProductCount = products.Count(p => p.CategoryId == c.Id)
        }).ToList();

        // Pass data to the view
        ViewBag.CategoryNames = categoryProductCount.Select(c => c.CategoryName).ToList();
        ViewBag.ProductCounts = categoryProductCount.Select(c => c.ProductCount).ToList();

        return View();
    }
}