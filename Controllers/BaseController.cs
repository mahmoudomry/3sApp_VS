using _3sApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _3sApp.Controllers
{
    public class BaseController : Controller
    {
        private readonly ApplicationDBContext _context;

        public BaseController(ApplicationDBContext context)
        {
             _context=context;
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
        }
  
    }
}
