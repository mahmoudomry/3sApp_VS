using _3sApp.Areas.Identity.Data;
using _3sApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _3sApp.Controllers
{
    public class NewsController : BaseController
    {

        private readonly ApplicationDBContext _context;
        public NewsController(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.current_controller = "Services";
            ViewBag.current_action = "Index";
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.ServicesLinks = _context.Services.Select(x => new { x.Id, x.Name }).ToList();
            return View();
        }
    }
}