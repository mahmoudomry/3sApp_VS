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
    public class ProjectsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProjectsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Administrative/Projects
        public async Task<IActionResult> Index()
        {
              return View(await _context.Projects.ToListAsync());
        }

        // GET: Administrative/Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Administrative/Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrative/Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create( Project project,IFormFile? clientLogo, IFormFile? CoverImage , IFormFile[]?OtherImages)
        {

            project.ClientLogo = "";
            project.CoverImage = "";
            UploadImage(project, CoverImage);
            UploadClientImage(project, clientLogo);

            if (ModelState.IsValid)
            {
                _context.Add(project);
                 _context.SaveChanges();
                if (OtherImages != null && OtherImages.Length > 0)
                {
                    UploadImages(project.Id, OtherImages);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Administrative/Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Administrative/Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Client,ClientLogo,Scope,IndustrialSolution,Describtion,CoverImage")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Administrative/Projects/Delete/5

        // POST: Administrative/Projects/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return _context.Projects.Any(e => e.Id == id);
        }

        private void UploadClientImage(Project project,IFormFile?clientlogo)
        {
            
            if (clientlogo !=null )
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(clientlogo.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                clientlogo.CopyTo(filestream);
                project.ClientLogo = ImageName;
            }
            else if (project.ClientLogo == null && project.Id == null)
            {
                project.ClientLogo = "main-slider-01.jpg";
            }
            else
            {
                project.ClientLogo = project.ClientLogo;
            }
        }
        private void UploadImage(Project project, IFormFile? coverImage)
        {

            if (coverImage != null)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(coverImage.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                coverImage.CopyTo(filestream);
                project.CoverImage = ImageName;
            }
            else if (project.CoverImage == null && project.Id == null)
            {
                project.CoverImage = "main-slider-01.jpg";
            }
            else
            {
                project.CoverImage = project.CoverImage;
            }
        }
        private void UploadImages(int projectId, IFormFile[]? otherImages)
        {

            if (otherImages != null)
            {
                int index = 1;

                List<Media> mlist=new List<Media>();
                foreach (var otherImage in otherImages)
                {
                       string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(otherImage.FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                    otherImage.CopyTo(filestream);
                    Media m = new Media();
                    m.TypeId = 1;
                    m.CategoryId = 1;
                    m.FileName = ImageName;
                    m.Order = index;
                    index++;
                    m.IsActive = true;
                    m.PostId = projectId;
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
