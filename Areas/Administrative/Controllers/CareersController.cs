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
    public class CareersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CareersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Administrative/Careers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Careers.ToListAsync());
        }

        // GET: Administrative/Careers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Careers == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // GET: Administrative/Careers/Create
        public IActionResult Create(int?id)
        {
            if(id!=null)
                return View(_context.Careers.Find(id));
            return View();
        }

        // POST: Administrative/Careers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( Career career)
        {

            if(career.Createdon==null)
            career.Createdon = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(career);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(career);
        }

   
      

        // POST: Administrative/Careers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Career career)
        {
            if (id != career.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(career);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareerExists(career.Id))
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
            return View("Create",career);
        }

      
        // POST: Administrative/Careers/Delete/5
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Careers == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Careers'  is null.");
            }
            var career = await _context.Careers.FindAsync(id);
            if (career != null)
            {
                _context.Careers.Remove(career);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareerExists(int id)
        {
          return _context.Careers.Any(e => e.Id == id);
        }
    }
}
