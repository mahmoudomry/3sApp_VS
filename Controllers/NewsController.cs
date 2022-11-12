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
            return View( _context.News.ToList());
           
        }
        public IActionResult Details(int? id)
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
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = _context.News
                .FirstOrDefault(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            List<Media> m = _context.Medias.Where(a => a.CategoryId == 2).ToList();
            news.media = m.Where(a => a.PostId == news.Id).ToList();


            return View(news);
        }
    }
}