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

namespace _3sApp.Areas.Administrative.Controllers
{
    [Area("Administrative")]
    [Authorize]
    public class NewsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public NewsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Administrative/News
        public async Task<IActionResult> Index()
        {
              return View(await _context.News.ToListAsync());
        }

        // GET: Administrative/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            List<Media> m = _context.Medias.Where(a => a.CategoryId == 2).ToList();
            news.media = m.Where(a => a.PostId == news.Id).ToList();
            return View(news);
        }

        // GET: Administrative/News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrative/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create( News news, IFormFile? clientLogo, IFormFile? CoverImage, IFormFile[]? OtherImages)
        {
            UploadImage(news, CoverImage);
            
            if (ModelState.IsValid)
            {
                _context.Add(news);
                 _context.SaveChanges();
                if (OtherImages != null && OtherImages.Length > 0)
                {
                    UploadImages(news.Id, OtherImages);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: Administrative/News/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news =  _context.News.Find(id);
            if (news == null)
            {
                return NotFound();
            }
            return View("Create",news);
        }

        // POST: Administrative/News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  News news, IFormFile? clientLogo, IFormFile? CoverImage, IFormFile[]? OtherImages)
        {
            if (id != news.Id)
            {
                return NotFound();
            }
            UploadImage(news, CoverImage);
            
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                     _context.SaveChanges();
                    if (OtherImages != null && OtherImages.Length > 0)
                    {
                        UploadImages(news.Id, OtherImages);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
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
            return View(news);
        }

      

        // POST: Administrative/News/Delete/5
        [ ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'ApplicationDBContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
          return _context.News.Any(e => e.Id == id);
        }

       
        private void UploadImage(News news, IFormFile? coverImage)
        {

            if (coverImage != null)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                coverImage.CopyTo(filestream);
                news.CoverImage = ImageName;
            }
            else if (news.CoverImage == null && news.Id == null)
            {
                news.CoverImage = "main-slider-01.jpg";
            }
            else
            {
                news.CoverImage = news.CoverImage;
            }
        }
        private void UploadImages(int NewsId, IFormFile[]? otherImages)
        {

            if (otherImages != null)
            {
                int index = 1;

                List<Media> mlist = new List<Media>();
                foreach (var otherImage in otherImages)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(otherImage.FileName);
                    var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                    otherImage.CopyTo(filestream);
                    Media m = new Media();
                    m.TypeId = 1;
                    m.CategoryId = 2;
                    m.FileName = ImageName;
                    m.Order = index;
                    index++;
                    m.IsActive = true;
                    m.PostId = NewsId;
                    m.Describtion = "";
                    m.Client = "";
                    m.Title = "";
                    mlist.Add(m);
                }
                if (mlist.Count > 0)
                {
                    _context.Medias.AddRange(mlist);
                    _context.SaveChanges();
                }
            }

        }
    }
}
