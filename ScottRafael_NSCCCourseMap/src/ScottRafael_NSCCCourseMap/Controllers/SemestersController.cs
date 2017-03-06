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
using Microsoft.AspNetCore.Authorization;

namespace ScottRafael_NSCCCourseMap.Controllers
{
    [Authorize]
    public class SemestersController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public SemestersController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: Semesters
        public async Task<IActionResult> Index()
        {
            var Semesters = from a in _context.Semesters.Include(s => s.AcademicYear) select a;
            Semesters = Semesters.OrderByDescending(s => s.StartDate);

            return View(await Semesters.ToListAsync());
        }

        // GET: Semesters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _context.Semesters
                .Include(s => s.AcademicYear)
                .Include(s => s.CourseOfferings).ThenInclude(s => s.Course)
                .Include(s => s.CourseOfferings).ThenInclude(s => s.Concentration)
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == id);
            if (semester == null)
            {
                return NotFound();
            }
            PopulateCourseOfferingData(semester);
            return View(semester);
        }

        private void PopulateCourseOfferingData(Semester semester)
        {
            var semesterCourseOfferings = semester.CourseOfferings.OrderBy(c=>c.Course.CourseFull).OrderBy(c=>c.Concentration.Title);
            var viewModel = new List<SemesterCourseDetailsData>();
            foreach (var courseOffering in semesterCourseOfferings)
            {
                viewModel.Add(new SemesterCourseDetailsData
                {
                    CourseCode = courseOffering.Course.CourseCode,
                    CourseTitle = courseOffering.Course.Title,
                    Concentration = courseOffering.Concentration.Title,
                    CampusCourse = courseOffering.IsCampusCourse

                });
            }
            ViewData["CourseOfferings"] = viewModel;
        }

        // GET: Semesters/Create
        public IActionResult Create()
        {
            PopulateAcademicYearsDropDownList();
            return View();
        }

        // POST: Semesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AcademicYearId,EndDate,Name,StartDate")] Semester semester)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semester);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateAcademicYearsDropDownList(semester.AcademicYearId);
            return View(semester);
        }

        // GET: Semesters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _context.Semesters.SingleOrDefaultAsync(m => m.Id == id);
            if (semester == null)
            {
                return NotFound();
            }
            PopulateAcademicYearsDropDownList(semester.AcademicYearId);
            return View(semester);
        }

        // POST: Semesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AcademicYearId,EndDate,Name,StartDate")] Semester semester)
        {
            if (id != semester.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semester);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemesterExists(semester.Id))
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
            PopulateAcademicYearsDropDownList(semester.AcademicYearId);
            return View(semester);
        }

        private void PopulateAcademicYearsDropDownList(object selectedAcademicYear = null)
        {
            var academicYearQuery = from a in _context.AcademicYears
                                orderby a.Title
                                select a;
            ViewBag.AcademicYearId = new SelectList(academicYearQuery.AsNoTracking(), "Id", "Title", selectedAcademicYear);
            //ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id");
        }

        // GET: Semesters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _context.Semesters.SingleOrDefaultAsync(m => m.Id == id);
            if (semester == null)
            {
                return NotFound();
            }

            return View(semester);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semester = await _context.Semesters.SingleOrDefaultAsync(m => m.Id == id);
            _context.Semesters.Remove(semester);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SemesterExists(int id)
        {
            return _context.Semesters.Any(e => e.Id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyName(string name)
        {
            if (_context.Semesters.Any(s => s.Name == name))
            {
                return Json(data: $"The name {name} is already in use.");
            }

            return Json(data: true);
        }
    }
}
