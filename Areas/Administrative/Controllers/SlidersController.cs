using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3sApp.Areas.Identity.Data;

using System.Security.Claims;
using _3sApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _3sApp.Areas.Administrative.Controllers
{
    [Area("Administrative")]
    [Authorize]
    public class SlidersController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SlidersController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Administrative/Sliders
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Sliders.Include(s => s.ApplicationUser);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Administrative/Sliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }

            var slider = await _context.Sliders
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            return View(slider);
        }

        // GET: Administrative/Sliders/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Administrative/Sliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public  IActionResult Create(Slider slider)
        {
            
            slider.UserId = _userManager.GetUserId(HttpContext.User);

            UploadImage(slider);
            if (ModelState.IsValid)
            {
                _context.Add(slider);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(slider);
        }

        private void UploadImage(Slider slider)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                slider.CoverImage = ImageName;
            }
            else if (slider.CoverImage == null && slider.Id == null)
            {
                slider.CoverImage = "main-slider-01.jpg";
            }
            else
            {
                slider.CoverImage = slider.CoverImage;
            }
        }

        // GET: Administrative/Sliders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Sliders == null)
            {
                return NotFound();
            }
           
            var slider =  _context.Sliders.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
          
            return View("Create",slider);
        }

        // POST: Administrative/Sliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Slider slider)
        {
            if (id != slider.Id)
            {
                return NotFound();
            }
            UploadImage(slider);
            slider.UserId = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slider);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SliderExists(slider.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
           
            return View("Create",slider);
        }

        //// GET: Administrative/Sliders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Sliders == null)
        //    {
        //        return NotFound();
        //    }

        //    var slider = await _context.Sliders
        //        .Include(s => s.ApplicationUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (slider == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(slider);
        //}

        // POST: Administrative/Sliders/Delete/5
        [ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sliders == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Sliders'  is null.");
            }
            var slider = await _context.Sliders.FindAsync(id);
            if (slider != null)
            {
                _context.Sliders.Remove(slider);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SliderExists(int id)
        {
          return _context.Sliders.Any(e => e.Id == id);
        }
    }
}
