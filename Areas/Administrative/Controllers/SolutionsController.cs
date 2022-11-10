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
    public class SolutionsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public SolutionsController(ApplicationDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Administrative/Solutions
        public async Task<IActionResult> Index()
        {
              return View(await _context.Solutions.ToListAsync());
        }
        public async Task<IActionResult> SubSolutions(int?Id)
        {
            if(Id!=null)
            return View(await _context.SubSolutions.Where(x=>x.SolutionId==Id.Value).Include(x=>x.Solution).ToListAsync());
            else
                return View(await _context.SubSolutions.Include(x => x.Solution).ToListAsync());
        }
        
        public IActionResult CreateSubSolutions(int? Id)
        {
            if (Id != null)
                ViewBag.Solutions = _context.Solutions.Where(x => x.Id == Id.Value).ToList();
            else
                ViewBag.Solutions = _context.Solutions.ToList();


            return View();
        }
        [HttpPost]
        public IActionResult CreateSubSolutions(SubSolution sub,IFormFile CoverImage,IFormFile Icon)
        {
            UploadImage(sub, CoverImage);
            UploadSubIcon(sub, Icon);
            if (ModelState.IsValid)
            {sub.CoverImage = "";
                _context.Add(sub);
                
                _context.SaveChanges();
                return RedirectToAction(nameof(SubSolutions), new { Id=sub.SolutionId});
            }
           
         ViewBag.Solutions = _context.Solutions.ToList();
            return View(sub);
        }
  
        public IActionResult EditSubSolutions(int? id)
        {
            //  UploadImage(sub);
            if (id == null || _context.SubSolutions == null)
            {
                return NotFound();
            }

            var sub = _context.SubSolutions.Find(id);
            if (sub == null)
            {
                return NotFound();
            }
            ViewBag.Solutions = _context.Solutions.ToList();
            return View("CreateSubSolutions", sub);
        }
        [HttpPost]
        public IActionResult EditSubSolutions(int? id,SubSolution sub, IFormFile CoverImage, IFormFile Icon)
        {

            if (id != sub.Id)
            {
                return NotFound();
            }
            UploadImage(sub, CoverImage);
            UploadSubIcon(sub, Icon);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sub);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolutionExists(sub.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SubSolutions), new { id=sub.SolutionId});
            }
            return View("CreateSubSolutions", sub);

           
        }
        // GET: Administrative/Solutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Solutions == null)
            {
                return NotFound();
            }

            var solution = await _context.Solutions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (solution == null)
            {
                return NotFound();
            }

            return View(solution);
        }

        // GET: Administrative/Solutions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrative/Solutions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Solution solution)
        {
            UploadImage(solution);
            if (ModelState.IsValid)
            {
                _context.Add(solution);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(solution);
        }

        // GET: Administrative/Solutions/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.Solutions == null)
            {
                return NotFound();
            }

            var solution =  _context.Solutions.Find(id);
            if (solution == null)
            {
                return NotFound();
            }
            return View("Create",solution);
        }

        // POST: Administrative/Solutions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,  Solution solution)
        {
            if (id != solution.Id)
            {
                return NotFound();
            }
            UploadImage(solution);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solution);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolutionExists(solution.Id))
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
            return View("Create", solution);
        }


        // POST: Administrative/Solutions/Delete/5
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Solutions == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Solutions'  is null.");
            }
            var solution = await _context.Solutions.FindAsync(id);
            if (solution != null)
            {
                _context.Solutions.Remove(solution);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

  

        public async Task<IActionResult> DeleteSubSolutions(int id)
        {
            int? solutionId=0;
            if (_context.SubSolutions == null)
            {
                return Problem("Entity set  is null.");
            }
            var solution = await _context.SubSolutions.FindAsync(id);
            if (solution != null)
            {
                solutionId = solution.SolutionId;
                _context.SubSolutions.Remove(solution);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SubSolutions), new {Id= solutionId.Value });
        }

        private bool SolutionExists(int id)
        {
          return _context.Solutions.Any(e => e.Id == id);
        }

        private void UploadImage(Solution solution)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                solution.CoverImage = ImageName;
            }
            else if (solution.CoverImage == null && solution.Id == null)
            {
                solution.CoverImage = "main-slider-01.jpg";
            }
            else
            {
                solution.CoverImage = solution.CoverImage;
            }
        }
        private void UploadImage(SubSolution sub,IFormFile formFile)
        {
           
            if (formFile!=null)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                formFile.CopyTo(filestream);
                sub.CoverImage = ImageName;
            }
            else if (sub.CoverImage == null && sub.Id == null)
            {
                sub.CoverImage = "main-slider-01.jpg";
            }
            else
            {
                sub.CoverImage = sub.CoverImage;
            }
        }
        private void UploadSubIcon(SubSolution sub, IFormFile formFile)
        {
            
            if (formFile!=null)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                formFile.CopyTo(filestream);
                sub.Icon = ImageName;
            }
            else if (sub.Icon == null && sub.Id == null)
            {
                sub.Icon = "3srediconlogo.svg";
            }
            else
            {
                sub.Icon = sub.Icon;
            }
        }
    }
}
