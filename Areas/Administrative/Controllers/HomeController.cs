using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using _3sApp.Models;
using _3sApp.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace _3sApp.Areas.Administrative.Controllers
{
    [Area("Administrative")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult SiteSetting ()
        {
            SiteSetting Setting = _context.SiteSettings.FirstOrDefault();
            return View(Setting);
        }
        public ActionResult About()
        {
            About about = _context.Abouts.FirstOrDefault();
            return View(about);
        }

        public ActionResult OurValues()
        {
            var ourValues = _context.OurValues.ToList();
            return View(ourValues);
        }

        public ActionResult EditValues(int? id)
        {
            var Value = _context.OurValues.Find(id);
            if (Value == null)
            {
                return View();
            }
            
            return View(Value);
        }
        [HttpPost]
        public ActionResult EditValues(OurValue value,IFormFile? Iconfile)
        {
            
            UploadvalueIcon(value, Iconfile);
            if (ModelState.IsValid)
            {   try
                {

                    if (value.Id == 0)
                    {
                        _context.Add(value);
                    }
                    else
                    {
                        _context.Update(value);
                    }
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                    
                }
                return RedirectToAction(nameof(OurValues));
            }
            return View("EditValues", value);
        }
        [ActionName("DeleteValues")]
       
        public ActionResult DeleteValues(int id)
        {
            if (_context.OurValues == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Projects'  is null.");
            }
            var Value =  _context.OurValues.Find(id);
            if (Value != null)
            {
                _context.OurValues.Remove(Value);
            }

             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public ActionResult About(int id,About about,IFormFile? HomeImage, IFormFile? PageImage, IFormFile? SecurityImage,
            IFormFile? CEOImage, IFormFile? SignImage)
        {
            if (id != about.Id)
            {
                return NotFound();
            }
            if(HomeImage != null)
            about.HomeImage = UploadImage(HomeImage);
            if (PageImage != null)
                about.PageImage = UploadImage(PageImage);
            if (SecurityImage != null)
                about.SecurityImage = UploadImage(SecurityImage);
            if (CEOImage != null)
                about.CEOImage = UploadImage(CEOImage);
            if (SignImage != null)
                about.SignImage = UploadImage(SignImage);

           
                     _context.Update(about);
                     _context.SaveChanges();
                return RedirectToAction(nameof(Index));
           
          
        }

        [HttpPost]
        public ActionResult SiteSetting(int id, SiteSetting site, IFormFile? HeaderLogo, IFormFile? FooterLogo,IFormFile? HomeHeaderLogo)
        {
            if (id != site.Id)
            {
                return NotFound();
            }
            if(HomeHeaderLogo != null)
                site.HomeHeaderLogo = UploadImage(HomeHeaderLogo);
            if (HeaderLogo != null)
                site.HeaderLogo = UploadImage(HeaderLogo);
            if (FooterLogo != null)
                site.FooterLogo = UploadImage(FooterLogo);
           
            _context.Update(site);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
        private void UploadvalueIcon(OurValue value,IFormFile?Icon)
        {
            
            if (Icon!=null)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(Icon.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                Icon.CopyTo(filestream);
                value.Icon = ImageName;
            }
            else if (value.Icon == null && value.Id == null)
            {
                value.Icon = "main-slider-01.jpg";
            }
            else
            {
                value.Icon = value.Icon;
            }
        }
        private string UploadImage(IFormFile? file)
        {
            if (file != null)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file.CopyTo(filestream);
                return ImageName;
            }
return "";  
        }   



    }

}
