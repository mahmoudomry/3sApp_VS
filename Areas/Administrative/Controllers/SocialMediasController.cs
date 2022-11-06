using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3sApp.Areas.Identity.Data;
using _3sApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace _3sApp.Areas.Administrative.Controllers
{
    [Area("Administrative")]
    [Authorize]
    public class SocialMediasController : Controller
    {
        private readonly ApplicationDBContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public SocialMediasController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Administrative/SocialMedias
        public async Task<IActionResult> Index()
        {
              return View(await _context.SocialMedias.ToListAsync());
        }

        // GET: Administrative/SocialMedias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SocialMedias == null)
            {
                return NotFound();
            }

            var SocialMedia = await _context.SocialMedias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (SocialMedia == null)
            {
                return NotFound();
            }

            return View(SocialMedia);
        }

        // GET: Administrative/SocialMedias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrative/SocialMedias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create( SocialMedia socialMedia)
        {
           // UploadImage(socialMedia);
            if (ModelState.IsValid)
            {
                _context.Add(socialMedia);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(socialMedia);
        }

        // GET: Administrative/SocialMedias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SocialMedias == null)
            {
                return NotFound();
            }

            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia == null)
            {
                return NotFound();
            }
            return View( "Create", socialMedia);
        }

        // POST: Administrative/SocialMedias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SocialMedia socialMedia)
        {
            if (id != socialMedia.Id)
            {
                return NotFound();
            }
           // UploadImage(socialMedia);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socialMedia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialMediaExists(socialMedia.Id))
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
            return View("Create", socialMedia);
        }


        // POST: Administrative/SocialMedias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SocialMedias == null)
            {
                return Problem("Entity set 'ApplicationDBContext.SocialMedias'  is null.");
            }
            var socialMedia = await _context.SocialMedias.FindAsync(id);
            if (socialMedia != null)
            {
                _context.SocialMedias.Remove(socialMedia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialMediaExists(int id)
        {
          return _context.SocialMedias.Any(e => e.Id == id);
        }
        private void UploadImage(SocialMedia socialMedia)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                socialMedia.Icon = ImageName;
            }
            else if (socialMedia.Icon == null && socialMedia.Id == null)
            {
                socialMedia.Icon = "main-slider-01.jpg";
            }
            else
            {
                socialMedia.Icon = socialMedia.Icon;
            }
        }
    }
}
