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
using ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels;

namespace ScottRafael_NSCCCourseMap.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public CoursesController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: Courses
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["CodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "code_desc" : "";
            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["CurrentFilter"] = searchString;

            var courses = from c in _context.Courses
                          select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.CourseCode.Contains(searchString)
                                       || c.Title.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "code_desc":
                    courses = courses.OrderByDescending(c => c.CourseCode);
                    break;
                case "Title":
                    courses = courses.OrderBy(c => c.Title);
                    break;
                case "title_desc":
                    courses = courses.OrderByDescending(c => c.Title);
                    break;
                default:
                    courses = courses.OrderBy(c => c.CourseCode);
                    break;
            }
            return View(await courses.AsNoTracking().ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            var viewModel = new CourseInformation();
            viewModel.Id = (int)id;
            viewModel.Course = course;
            viewModel.CoursePreRequisites = await _context.CoursePreRequisites
                                           .Include(c => c.Course)
                                           .Include(c => c.PreRequisite)
                                           .Where(c => c.PreRequisiteId.Equals(id))
                                           .AsNoTracking()
                                           .OrderBy(c => c.Course.CourseCode)
                                           .ToListAsync();
            viewModel.PreRequisiteFor = await _context.CoursePreRequisites
                                           .Include(c => c.Course)
                                           .Include(c => c.PreRequisite)
                                           .Where(c => c.CourseId.Equals(id))
                                           .AsNoTracking()
                                           .OrderBy(c => c.Course.CourseCode)
                                           .ToListAsync();

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseCode,Title")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseCode,Title")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }

        //[AcceptVerbs("Get", "Post")]
        //public IActionResult VerifyTitle(string title)
        //{
        //    if (_context.Courses.Any(c => c.Title == title))
        //    {
        //        return Json(data: $"The title {title} is already in use.");
        //    }

        //    return Json(data: true);
        //}

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyCourseCode(string code)
        {
            if (_context.Courses.Any(c => c.CourseCode == code))
            {
                return Json(data: $"The course code {code} is already in use.");
            }

            return Json(data: true);
        }

    }
}
