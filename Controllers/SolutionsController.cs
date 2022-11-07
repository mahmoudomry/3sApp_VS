using _3sApp.Areas.Identity.Data;
using _3sApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace _3sApp.Controllers
{
    public class SolutionsController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDBContext _context;
        public SolutionsController(ILogger<HomeController> logger,ApplicationDBContext context):base(context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int?id)
        {
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            if (id != null)
                ViewBag.Solutions = _context.Solutions.Where(x=>x.Id==id.Value).Include(x => x.SubSolution).FirstOrDefault();
            else
                ViewBag.Solutions = _context.Solutions.Include(x => x.SubSolution).FirstOrDefault(); 
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