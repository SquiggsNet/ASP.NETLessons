using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScottRafael_NSCCCourseMap.Data;
using ScottRafael_NSCCCourseMap.Models;
using Microsoft.AspNetCore.Authorization;

namespace ScottRafael_NSCCCourseMap.Controllers
{
    [Authorize]
    public class ProgramsController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public ProgramsController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: Programs
        public async Task<IActionResult> Index()
        {
            var Programs = from p in _context.Programs
                                select p;
            Programs = Programs.OrderBy(p => p.Title);
            return View(await Programs.ToListAsync());
        }

        // GET: Programs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs
                .Include(p => p.Concentrations)        
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            
            if (program == null)
            {
                return NotFound();
            }
            program.Concentrations = program.Concentrations.OrderBy(c => c.Title).ToList();

            return View(program);
        }

        // GET: Programs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Programs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] ScottRafael_NSCCCourseMap.Models.Program program)
        {
            if (ModelState.IsValid)
            {
                _context.Add(program);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        // GET: Programs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs.SingleOrDefaultAsync(m => m.Id == id);
            if (program == null)
            {
                return NotFound();
            }
            return View(program);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] ScottRafael_NSCCCourseMap.Models.Program program)
        {
            if (id != program.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(program);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramExists(program.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(program);
        }

        // GET: Programs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs.SingleOrDefaultAsync(m => m.Id == id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = await _context.Programs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Programs.Remove(program);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProgramExists(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyTitle(string title)
        {
            if (_context.Programs.Any(p => p.Title == title))
            {
                return Json(data: $"The title {title} is already in use.");
            }

            return Json(data: true);
        }
    }
}
