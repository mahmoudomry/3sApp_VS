using _3sApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _3sApp.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ServicesController(ILogger<HomeController> logger)
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