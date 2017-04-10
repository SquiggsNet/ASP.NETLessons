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
using ScottRafael_NSCCCourseMap.Models.DTOs;
using Microsoft.AspNetCore.Cors;

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
            var semesterCourseOfferings = semester.CourseOfferings.OrderBy(c => c.Course.CourseFull).OrderBy(c => c.Concentration.Title);
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

        //API Methods

        //api/Semesters

        /// <summary>
        /// Returns a collection of Semesters.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Semesters")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetSemesters()
        {
            //return a list of concentrations
            List<SemesterDTO> dtoList = new List<SemesterDTO>();

            var Semesters = await _context.Semesters
                            .OrderByDescending(s => s.StartDate)
                            .AsNoTracking()
                            .ToListAsync();

            foreach (var Semester in Semesters)
            {
                var dtoSemester = new SemesterDTO
                {
                    Id = Semester.Id,
                    Name = Semester.Name,
                    StartDate = Semester.StartDate,
                    EndDate = Semester.EndDate
                };
                dtoList.Add(dtoSemester);
            };
            return new ObjectResult(dtoList);
        }

        //api/Semesters/id

        /// <summary>
        /// Returns a specific Semester. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Semesters/{id}")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetSemester(int? id)
        {
            var Semester = await _context.Semesters
                        .Include(s => s.CourseOfferings)
                            .ThenInclude(c => c.Course)
                        .Include(c => c.CourseOfferings)
                            .ThenInclude(c => c.Concentration)
                        .Include(s => s.AcademicYear)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(s => s.Id == id);

            if (Semester == null)
            {
                return NotFound();
            }

            var SemesterFullDTO = new SemesterFullDTO
            {
                Id = Semester.Id,
                Name = Semester.Name,
                AcademicYear = Semester.AcademicYear.Title,
                StartDate = Semester.StartDate,
                EndDate = Semester.EndDate
            };
            List<CourseOfferingsDTO> courseOfferingDtoList = new List<CourseOfferingsDTO>();
            foreach (var courseOffering in Semester.CourseOfferings)
            {
                var CourseOfferingsDTO = new CourseOfferingsDTO
                {
                    Id = courseOffering.Id,
                    CourseId = courseOffering.CourseId,
                    CourseTitle = courseOffering.Course.Title,
                    ConcentrationId = courseOffering.ConcentrationId,
                    ConcentrationTitle = courseOffering.Concentration.Title,
                    SemesterId = courseOffering.SemesterId,
                    SemesterName = courseOffering.Semester.Name,
                    IsCampusCourse = courseOffering.IsCampusCourse
                };
                courseOfferingDtoList.Add(CourseOfferingsDTO);
            };
            SemesterFullDTO.CourseOfferings = courseOfferingDtoList;

           

            return new ObjectResult(SemesterFullDTO);
        }
    }
}
