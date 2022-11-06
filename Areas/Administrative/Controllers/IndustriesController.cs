using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3sApp.Areas.Identity.Data;
using _3sApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace _3sApp.Controllers
{
    [Area("Administrative")]
    [Authorize]
    public class IndustriesController : Controller
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public IndustriesController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: Industries
        public async Task<IActionResult> Index()
        {
              return View(await _context.Industries.ToListAsync());
        }

        // GET: Industries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Industries == null)
            {
                return NotFound();
            }

            var industry = await _context.Industries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (industry == null)
            {
                return NotFound();
            }

            return View(industry);
        }

        // GET: Industries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Industries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create( Industry industry)
        {
            UploadImage(industry);
            UploadIcon(industry);
            if (ModelState.IsValid)
            {
                _context.Add(industry);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(industry);
        }

        // GET: Industries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Industries == null)
            {
                return NotFound();
            }

            var industry = await _context.Industries.FindAsync(id);
            if (industry == null)
            {
                return NotFound();
            }
            return View("Create", industry);
        }

        // POST: Industries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Title,Describtion,CoverImage,Icon")] Industry industry)
        {
            if (id != industry.Id)
            {
                return NotFound();
            }
            UploadImage(industry);
            UploadIcon(industry);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(industry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndustryExists(industry.Id))
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
            return View("Create", industry);
       
        }

 

        // POST: Industries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.Industries == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Industries'  is null.");
            }
            var industry = await _context.Industries.FindAsync(id);
            if (industry != null)
            {
                _context.Industries.Remove(industry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndustryExists(int? id)
        {
          return _context.Industries.Any(e => e.Id == id);
        }

        private void UploadImage(Industry industry)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                industry.CoverImage = ImageName;
            }
            else if (industry.CoverImage == null && industry.Id == null)
            {
                industry.CoverImage = "main-slider-01.jpg";
            }
            else
            {
                industry.CoverImage = industry.CoverImage;
            }
        }
        private void UploadIcon(Industry industry)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 1)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[1].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file[1].CopyTo(filestream);
                industry.Icon = ImageName;
            }
            else if (industry.Icon == null && industry.Id == null)
            {
                industry.Icon = "main-slider-01.jpg";
            }
            else
            {
                industry.Icon = industry.Icon;
            }
        }
    }
}
