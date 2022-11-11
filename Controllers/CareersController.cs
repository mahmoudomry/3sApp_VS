using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3sApp.Areas.Identity.Data;
using _3sApp.Models;

namespace _3sApp.Views.Home
{
    public class CareersController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CareersController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Careers
        public async Task<IActionResult> Index()
        {
              return View(await _context.Careers.ToListAsync());
        }

        // GET: Careers/Details/5
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

       
        private bool CareerExists(int id)
        {
          return _context.Careers.Any(e => e.Id == id);
        }
    }
}
