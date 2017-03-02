using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScottRafael_NSCCCourseMap.Data;
using ScottRafael_NSCCCourseMap.Models;
using ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels;

namespace ScottRafael_NSCCCourseMap.Controllers
{
    public class CoursePreRequisitesController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public CoursePreRequisitesController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: CoursePreRequisites
        public async Task<IActionResult> Index(int ? selectedCourseID)
        {
            //var viewModel = new CoursePrerequisiteData();

            //var courses = await _context.CoursePreRequisites
            //                .Include(c => c.Course)
            //                .AsNoTracking()
            //                .OrderBy(c => c.Course.Title)
            //                .ToListAsync();

            //if(selectedCourseID != null)
            //{
            //    var selectCourseID = (int)selectedCourseID;
            //    courses = await _context.CoursePreRequisites
            //                .Include(c => c.Course)
            //                .AsNoTracking()
            //                .OrderBy(c => c.Course.Title)
            //                .Where(c => c.Course.Id.Equals(selectCourseID))
            //                .ToListAsync();                  
            //}

            //ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "CourseFull");
            //PopulatePrerequisiteCourseData;

            var allPrerequisites = _context.CoursePreRequisites
                                .Include(c => c.Course)
                                .AsNoTracking();
            var allCourses = _context.Courses;


            var viewModel = new List<CoursePrerequisiteData>();
            foreach (var currentPre in allPrerequisites)
            {
                var id = currentPre.PreRequisiteId;
                var Prereq = allCourses.AsNoTracking().Single(m => m.Id == id);

                viewModel.Add(new CoursePrerequisiteData
                {
                    Id = currentPre.Id,
                    Course = currentPre.Course,
                    PrerequisiteCourse = Prereq
                });
            }
            ViewData["PopCourses"] = viewModel;

            return View(viewModel);
        }

        private void PopulatePrerequisiteCourseData(Course course)
        {
            var allPrerequisites = _context.CoursePreRequisites
                                .Include(c => c.Course)
                                .AsNoTracking();
            var allCourses = _context.Courses;


            var viewModel = new List<CoursePrerequisiteData>();
            foreach (var currentPre in allPrerequisites)
            {
                var id = currentPre.PreRequisiteId;
                var Prereq = allCourses.AsNoTracking().Single(m => m.Id == id);
     
                viewModel.Add(new CoursePrerequisiteData
                {
                   Id = currentPre.Id,
                   Course = currentPre.Course,
                   PrerequisiteCourse = Prereq                   
                });
            }
            ViewData["Courses"] = viewModel;
        }

        // GET: CoursePreRequisites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursePreRequisite = await _context.CoursePreRequisites.SingleOrDefaultAsync(m => m.Id == id);
            if (coursePreRequisite == null)
            {
                return NotFound();
            }

            return View(coursePreRequisite);
        }

        // GET: CoursePreRequisites/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            return View();
        }

        // POST: CoursePreRequisites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,PreRequisiteId")] CoursePreRequisite coursePreRequisite)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coursePreRequisite);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", coursePreRequisite.CourseId);
            return View(coursePreRequisite);
        }

        // GET: CoursePreRequisites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursePreRequisite = await _context.CoursePreRequisites.SingleOrDefaultAsync(m => m.Id == id);
            if (coursePreRequisite == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", coursePreRequisite.CourseId);
            return View(coursePreRequisite);
        }

        // POST: CoursePreRequisites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,PreRequisiteId")] CoursePreRequisite coursePreRequisite)
        {
            if (id != coursePreRequisite.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coursePreRequisite);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoursePreRequisiteExists(coursePreRequisite.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", coursePreRequisite.CourseId);
            return View(coursePreRequisite);
        }

        // GET: CoursePreRequisites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coursePreRequisite = await _context.CoursePreRequisites.SingleOrDefaultAsync(m => m.Id == id);
            if (coursePreRequisite == null)
            {
                return NotFound();
            }

            return View(coursePreRequisite);
        }

        // POST: CoursePreRequisites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coursePreRequisite = await _context.CoursePreRequisites.SingleOrDefaultAsync(m => m.Id == id);
            _context.CoursePreRequisites.Remove(coursePreRequisite);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CoursePreRequisiteExists(int id)
        {
            return _context.CoursePreRequisites.Any(e => e.Id == id);
        }
    }
}
