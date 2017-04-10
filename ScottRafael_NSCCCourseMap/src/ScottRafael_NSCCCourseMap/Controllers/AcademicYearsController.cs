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
using ScottRafael_NSCCCourseMap.Models.DTOs;
using Microsoft.AspNetCore.Cors;

namespace ScottRafael_NSCCCourseMap.Controllers
{
    [Authorize]
    public class AcademicYearsController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public AcademicYearsController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: AcademicYears
        public async Task<IActionResult> Index()
        {
            var AcademicYears = from a in _context.AcademicYears
                           select a;
            AcademicYears = AcademicYears.OrderBy(a => a.Title);
            return View(await AcademicYears.ToListAsync());
        }

        // GET: AcademicYears/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears
                .Include(a => a.Semesters)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);

            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        // GET: AcademicYears/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title")] AcademicYear academicYear)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicYear);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(academicYear);
        }

        // GET: AcademicYears/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears.SingleOrDefaultAsync(m => m.Id == id);
            if (academicYear == null)
            {
                return NotFound();
            }
            return View(academicYear);
        }

        // POST: AcademicYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title")] AcademicYear academicYear)
        {
            if (id != academicYear.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicYear);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicYearExists(academicYear.Id))
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
            return View(academicYear);
        }

        // GET: AcademicYears/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var academicYear = await _context.AcademicYears.SingleOrDefaultAsync(m => m.Id == id);
            if (academicYear == null)
            {
                return NotFound();
            }

            return View(academicYear);
        }

        // POST: AcademicYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var academicYear = await _context.AcademicYears.SingleOrDefaultAsync(m => m.Id == id);
            _context.AcademicYears.Remove(academicYear);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool AcademicYearExists(int id)
        {
            return _context.AcademicYears.Any(e => e.Id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyTitle(string title)
        {
            if (_context.AcademicYears.Any(a => a.Title == title))
            {
                return Json(data: $"The title {title} is already in use.");
            }

            return Json(data: true);
        }

        //API Methods

        //api/AcademicYears

        /// <summary>
        /// Returns a collection of Academic Years.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/AcademicYears")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetAcademicYears()
        {
            //return a list of concentrations
            List<AcademicYearDTO> dtoList = new List<AcademicYearDTO>();

            var AcademicYears = await _context.AcademicYears
                        .Include(a => a.Semesters)
                        .OrderBy(a => a.Title) 
                        .AsNoTracking()
                        .ToListAsync();

            foreach (var AcademicYear in AcademicYears)
            {

                var dto = new AcademicYearDTO
                {
                    Id = AcademicYear.Id,
                    Title = AcademicYear.Title,
                };

                List<SemesterDTO> semesterDtoList = new List<SemesterDTO>();
                foreach (var Semester in AcademicYear.Semesters.OrderBy(s =>s.StartDate))
                {
                    var dtoSemester = new SemesterDTO
                    {
                        Id = Semester.Id,
                        Name = Semester.Name,
                        StartDate = Semester.StartDate,
                        EndDate = Semester.EndDate
                    };
                    semesterDtoList.Add(dtoSemester);
                };

                dto.Semesters = semesterDtoList;
                dtoList.Add(dto);
            }
            return new ObjectResult(dtoList);
        }

        //api/AcademicYears/id

        /// <summary>
        /// Returns a specific Academic Year. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/AcademicYears/{id}")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetAcademicYear(int? id)
        {
            var AcademicYear = await _context.AcademicYears
                        .Include(a => a.Semesters)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(m => m.Id == id);

            if (AcademicYear == null)
            {
                return NotFound();
            }

            var dtoAcademicYear = new AcademicYearDTO
            {
                Id = AcademicYear.Id,
                Title = AcademicYear.Title,
            };

            List<SemesterDTO> semesterDtoList = new List<SemesterDTO>();
            foreach (var Semester in AcademicYear.Semesters.OrderByDescending(s => s.StartDate))
            {
                var dtoSemester = new SemesterDTO
                {
                    Id = Semester.Id,
                    Name = Semester.Name,
                    StartDate = Semester.StartDate,
                    EndDate = Semester.EndDate
                };
                semesterDtoList.Add(dtoSemester);
            };

            dtoAcademicYear.Semesters = semesterDtoList;



            return new ObjectResult(dtoAcademicYear);
        }
    }
}
