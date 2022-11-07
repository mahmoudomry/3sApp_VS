using _3sApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using _3sApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
using _3sApp.ViewModels;
namespace _3sApp.Controllers
{
    public class HomeController : BaseController
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;
//ILogger<HomeController> logger, 
        public HomeController(ApplicationDBContext context) : base(context)
        {
            //_logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            HomeViewModel homeviwemodel = new HomeViewModel();
            homeviwemodel.Sliders = _context.Sliders.OrderBy(x => x.Order).ToList();
            homeviwemodel.About= _context.Abouts.FirstOrDefault();
            homeviwemodel.SiteSettings = _context.SiteSettings.FirstOrDefault();
            homeviwemodel.Contactitems = _context.Contactitems.OrderBy(x => x.Order).ToList();
            homeviwemodel.SocialMedias = _context.SocialMedias.OrderBy(x => x.Order).ToList();
            homeviwemodel.Services = _context.Services.ToList();
            homeviwemodel.Solutions = _context.Solutions.Include(x=>x.SubSolution).ToList();
            homeviwemodel.Projects = _context.Projects.Include(x => x.Images.Where(x=>x.CategoryId==1)).ToList();
            homeviwemodel.Industries = _context.Industries.ToList();
            homeviwemodel.Partners = _context.Partners.ToList();
            homeviwemodel.Clients = _context.Clients.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();

            ViewBag.current_controller = "Home";
            ViewBag.current_action = "Index";
            return View(homeviwemodel);
        }
        public IActionResult Index2()
        {
            ViewBag.current_controller = "Home";
            ViewBag.current_action = "Index";
            ViewBag.Sliders = _context.Sliders.ToList();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.Services = _context.Services.ToList();
            ViewBag.Solutions = _context.Solutions.Include(x => x.SubSolution).ToList();
            ViewBag.Projects = _context.Projects.Include(x => x.Images.Where(x => x.CategoryId == 1)).ToList();
            ViewBag.Industries = _context.Industries.ToList();
            ViewBag.Partners = _context.Partners.ToList();
            ViewBag.Clients = _context.Clients.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x=> new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            return View();
        }
        public IActionResult Indexhtml ()
        {
            return View();
        }

            public IActionResult Solutions()
        {  
            return View();
        }
        public IActionResult Partners()
        {
            ViewBag.current_controller = "Home";
            ViewBag.current_action = "Partners";
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.Partners = _context.Partners.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            return View();
        }
        public IActionResult Industries(int?id)
        {
            ViewBag.current_controller = "Home";
            ViewBag.current_action = "Industries";
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.Industries = _context.Industries.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            return View();
        }
        public IActionResult Contact()
        {
            ViewBag.current_controller = "Home";
            ViewBag.current_action = "Contact";
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.ContactitemsPage = _context.Contactitems.Where(x=> x.Title== "Phone"|| x.Title == "Fax"|| x.Title == "Mail us" || x.Title == "P.O. Box").ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            return View();
        }
        public IActionResult About()
        {
            ViewBag.current_controller = "Home";
            ViewBag.current_action = "About";
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.OurValues = _context.OurValues.ToList();
            ViewBag.Members = _context.Members.OrderByDescending(x=>x.Order).ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();
            return View();
        }
        public IActionResult Projects()
        {
            ViewBag.current_controller = "Home";
            ViewBag.current_action = "Projects";
            ViewBag.Projects = _context.Projects.Include(x => x.Images.Where(x => x.CategoryId == 1)).ToList();
            ViewBag.About = _context.Abouts.FirstOrDefault();
            ViewBag.SiteSettings = _context.SiteSettings.FirstOrDefault();
            ViewBag.Contactitems = _context.Contactitems.ToList();
            ViewBag.SocialMedias = _context.SocialMedias.ToList();
            ViewBag.SolutionsLinks = _context.Solutions.Select(x => new { x.Id, x.Title }).ToList();
            ViewBag.IndustrialLinks = _context.Industries.Select(x => new { x.Id, x.Title }).ToList();

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}