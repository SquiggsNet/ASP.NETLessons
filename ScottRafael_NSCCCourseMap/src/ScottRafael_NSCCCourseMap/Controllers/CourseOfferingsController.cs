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
    public class CourseOfferingsController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public CourseOfferingsController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: CourseOfferings
        public async Task<IActionResult> Index(int? SelectedConcentrationID, int? SelectedSemesterID)
        {
            
            var viewModel = new CourseOfferingIndexData();

            //if (SelectedSemesterID != null)
            //{
            //    viewModel.SelectedSemesterID = (int)SelectedSemesterID;
            //    viewModel.CourseOfferings = await _context.CourseOfferings
            //                .Include(c => c.Course)
            //                .Include(c => c.Concentration)
            //                .Include(c => c.Semester)
            //                .Where(c => c.Semester.Id.Equals(SelectedSemesterID))
            //                .AsNoTracking()
            //                .OrderBy(c => c.Semester.Name)
            //                .ToListAsync();
            //    // viewModel.Concentrations = _context.Concentrations.Where(c=> c.CourseOfferings.Where(c=>c.Semester.Id.Equals(SelectedSemesterID)));



            //    //    ; viewModel.CourseOfferings.Where(s => s.Semester.Id.Equals(SelectedSemesterID));                   

            //    SelectList concentrationList = new SelectList(_context.Concentrations, "Id", "Title");
            //    viewModel.ConcentrationList = concentrationList;
            //}

            //if (SelectedConcentrationID != null)
            //{
            //    viewModel.SelectedConcentrationID = (int)SelectedConcentrationID;
            //    viewModel.CourseOfferings = await _context.CourseOfferings
            //                .Include(c => c.Course)
            //                .Include(c => c.Concentration)
            //                .Include(c => c.Semester)
            //                .Where(c => c.Semester.Id.Equals(SelectedSemesterID))
            //                .Where(c => c.Concentration.Id.Equals(SelectedConcentrationID))
            //                .AsNoTracking()
            //                .OrderBy(c => c.Semester.Name)
            //                .ToListAsync();
            //}

            if (SelectedSemesterID != null && SelectedConcentrationID != null)
            {
                viewModel.SelectedSemesterID = (int)SelectedSemesterID;
                viewModel.SelectedConcentrationID = (int)SelectedConcentrationID;
                viewModel.CourseOfferings = await NewMethod()
                            .Where(c => c.Semester.Id.Equals(SelectedSemesterID))
                            .Where(c => c.Concentration.Id.Equals(SelectedConcentrationID))
                            .AsNoTracking()
                            .OrderBy(c => c.Semester.Name)
                            .ToListAsync();
            }
            else
            {
                if (SelectedSemesterID != null)
                {
                    viewModel.SelectedSemesterID = (int)SelectedSemesterID;
                    viewModel.CourseOfferings = await _context.CourseOfferings
                                .Include(c => c.Course)
                                .Include(c => c.Concentration)
                                .Include(c => c.Semester)
                                .Where(c => c.Semester.Id.Equals(SelectedSemesterID))
                                .AsNoTracking()
                                .OrderBy(c => c.Semester.Name)
                                .ToListAsync();
                }

                if (SelectedConcentrationID != null)
                {
                    viewModel.SelectedConcentrationID = (int)SelectedConcentrationID;
                    viewModel.CourseOfferings = await _context.CourseOfferings
                                .Include(c => c.Course)
                                .Include(c => c.Concentration)
                                .Include(c => c.Semester)
                                .Where(c => c.Concentration.Id.Equals(SelectedConcentrationID))
                                .AsNoTracking()
                                .OrderBy(c => c.Semester.Name)
                                .ToListAsync();
                }
            }



            SelectList semesterList = new SelectList(_context.Semesters.OrderByDescending(s=>s.StartDate), "Id", "Name");
            viewModel.SemesterList = semesterList;

            SelectList concentrationList = new SelectList(_context.Concentrations.OrderBy(c => c.Title), "Id", "Title");
            viewModel.ConcentrationList = concentrationList;

            return View(viewModel);
        }

        private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<CourseOffering, Semester> NewMethod()
        {
            return _context.CourseOfferings
                                        .Include(c => c.Course)
                                        .Include(c => c.Concentration)
                                        .Include(c => c.Semester);
        }

        // GET: CourseOfferings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseOffering = await _context.CourseOfferings
                .Include(c => c.Course)
                .Include(c => c.Concentration)
                .Include(c => c.Semester)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (courseOffering == null)
            {
                return NotFound();
            }

            return View(courseOffering);
        }

        // GET: CourseOfferings/Create
        public IActionResult Create()
        {
            ViewData["ConcentrationId"] = new SelectList(_context.Concentrations.OrderBy(c=>c.Title), "Id", "Title");
            ViewData["CourseId"] = new SelectList(_context.Courses.OrderBy(c=>c.CourseFull), "Id", "CourseFull");
            ViewData["SemesterId"] = new SelectList(_context.Semesters.OrderByDescending(s => s.StartDate), "Id", "Name");
            return View();
        }

        // POST: CourseOfferings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConcentrationId,CourseId,IsCampusCourse,SemesterId")] CourseOffering courseOffering)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseOffering);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ConcentrationId"] = new SelectList(_context.Concentrations.OrderBy(c => c.Title), "Id", "Title", courseOffering.ConcentrationId);
            ViewData["CourseId"] = new SelectList(_context.Courses.OrderBy(c => c.CourseFull), "Id", "CourseFull", courseOffering.CourseId);
            ViewData["SemesterId"] = new SelectList(_context.Semesters.OrderByDescending(s => s.StartDate), "Id", "Name", courseOffering.SemesterId);
            return View(courseOffering);
        }

        // GET: CourseOfferings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseOffering = await _context.CourseOfferings.SingleOrDefaultAsync(m => m.Id == id);
            if (courseOffering == null)
            {
                return NotFound();
            }
            ViewData["ConcentrationId"] = new SelectList(_context.Concentrations.OrderBy(c => c.Title), "Id", "Title", courseOffering.ConcentrationId);
            ViewData["CourseId"] = new SelectList(_context.Courses.OrderBy(c => c.CourseFull), "Id", "CourseFull", courseOffering.CourseId);
            ViewData["SemesterId"] = new SelectList(_context.Semesters.OrderByDescending(s => s.StartDate), "Id", "Name", courseOffering.SemesterId);
            return View(courseOffering);
        }

        // POST: CourseOfferings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConcentrationId,CourseId,IsCampusCourse,SemesterId")] CourseOffering courseOffering)
        {
            if (id != courseOffering.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseOffering);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseOfferingExists(courseOffering.Id))
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
            ViewData["ConcentrationId"] = new SelectList(_context.Concentrations.OrderBy(c => c.Title), "Id", "Title", courseOffering.ConcentrationId);
            ViewData["CourseId"] = new SelectList(_context.Courses.OrderBy(c => c.CourseFull), "Id", "CourseFull", courseOffering.CourseId);
            ViewData["SemesterId"] = new SelectList(_context.Semesters.OrderByDescending(s => s.StartDate), "Id", "Name", courseOffering.SemesterId);
            return View(courseOffering);
        }

        // GET: CourseOfferings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseOffering = await _context.CourseOfferings.SingleOrDefaultAsync(m => m.Id == id);
            if (courseOffering == null)
            {
                return NotFound();
            }

            return View(courseOffering);
        }

        // POST: CourseOfferings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseOffering = await _context.CourseOfferings.SingleOrDefaultAsync(m => m.Id == id);
            _context.CourseOfferings.Remove(courseOffering);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CourseOfferingExists(int id)
        {
            return _context.CourseOfferings.Any(e => e.Id == id);
        }
    }
}
