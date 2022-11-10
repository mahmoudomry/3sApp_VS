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
    public class ContactitemsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ContactitemsController(ApplicationDBContext context)
        {
            _context = context;
          
        }
        // GET: Administrative/Contactitems
        public async Task<IActionResult> Index()
        {
              return View(await _context.Contactitems.ToListAsync());
        }

        // GET: Administrative/Contactitems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Contactitems == null)
            {
                return NotFound();
            }

            var contactitem = await _context.Contactitems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactitem == null)
            {
                return NotFound();
            }

            return View(contactitem);
        }

        // GET: Administrative/Contactitems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrative/Contactitems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Contactitem contactitem)
        {
            UploadImage(contactitem);
            if (ModelState.IsValid)
            {
                _context.Add(contactitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactitem);
        }

        // GET: Administrative/Contactitems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Contactitems == null)
            {
                return NotFound();
            }

            var contactitem = await _context.Contactitems.FindAsync(id);
            if (contactitem == null)
            {
                return NotFound();
            }
            return View("Create",contactitem);
        }

        // POST: Administrative/Contactitems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Contactitem contactitem)
        {
            if (id != contactitem.Id)
            {
                return NotFound();
            }
            UploadImage(contactitem);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactitemExists(contactitem.Id))
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
            return View("Create", contactitem);
        }



        // POST: Administrative/Contactitems/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Contactitems == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Contactitem'  is null.");
            }
            var contactitem = await _context.Contactitems.FindAsync(id);
            if (contactitem != null)
            {
                _context.Contactitems.Remove(contactitem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactitemExists(int id)
        {
          return _context.Contactitems.Any(e => e.Id == id);
        }
        private void UploadImage(Contactitem contactitem)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(@"wwwroot/", "assets", "images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                contactitem.Icon = ImageName;
            }
            else if (contactitem.Icon == null && contactitem.Id == null)
            {
                contactitem.Icon = "main-slider-01.jpg";
            }
            else
            {
                contactitem.Icon = contactitem.Icon;
            }
        }


       

        
    }
}
