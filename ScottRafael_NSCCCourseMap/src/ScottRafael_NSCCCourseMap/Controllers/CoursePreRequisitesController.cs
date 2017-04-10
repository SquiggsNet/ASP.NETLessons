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
            var viewModel = new CoursePrerequisiteData();

            if (selectedCourseID != null)
            {
                viewModel.SelectedCourseID = (int)selectedCourseID;
                viewModel.CoursePreRequisites = await _context.CoursePreRequisites
                                           .Include(c => c.Course)
                                           .Include(c => c.PreRequisite)
                                           .Where(c => c.Course.Id.Equals(selectedCourseID))
                                           .AsNoTracking()
                                           .OrderBy(c => c.Course.CourseCode)
                                           .ToListAsync();
            }
            else
            {
                viewModel.CoursePreRequisites = await _context.CoursePreRequisites
                                            .Include(c => c.Course)
                                            .Include(c => c.PreRequisite)
                                            .AsNoTracking()
                                            .OrderBy(c => c.Course.CourseCode)
                                            .ToListAsync();
            }

            SelectList courseList = new SelectList(_context.Courses.OrderBy(c => c.CourseCode), "Id", "CourseFull");
            viewModel.CourseList = courseList;

            return View(viewModel);
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
            ViewBag.CourseId = new SelectList(_context.Courses.OrderBy(c => c.CourseCode), "Id", "CourseFull");

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
            ViewBag.CourseId = new SelectList(_context.Courses.OrderBy(c => c.CourseCode), "Id", "CourseFull");
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

        //api/CoursePrerequisites

        /// <summary>
        /// Returns a collection of Course Prerequisites.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/CoursePrerequisites")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetCoursePrerequisites()
        {
            //return a list of course offerings
            List<CoursePrerequisitesDTO> dtoList = new List<CoursePrerequisitesDTO>();

            var CoursePrerequisites = await _context.CoursePreRequisites
                            .Include(c => c.Course)
                            .Include(c => c.PreRequisite)
                            .AsNoTracking()
                            .OrderBy(c => c.Course.CourseCode)
                            .ToListAsync();

            foreach (var Prerequisite in CoursePrerequisites)
            {
                var dtoCoursePrerequisite = new CoursePrerequisitesDTO
                {
                    Id = Prerequisite.Id,
                };
                
                var CourseDTO = new CourseDTO
                {
                    Id = Prerequisite.Course.Id,
                    Code = Prerequisite.Course.CourseCode,
                    Title = Prerequisite.Course.Title
                };
                dtoCoursePrerequisite.Course = CourseDTO;
                var PrerequisiteDTO = new CourseDTO
                {
                    Id = Prerequisite.PreRequisite.Id,
                    Code = Prerequisite.PreRequisite.CourseCode,
                    Title = Prerequisite.PreRequisite.Title
                };
                dtoCoursePrerequisite.Prerequisite = PrerequisiteDTO;
                dtoList.Add(dtoCoursePrerequisite);
            };
            return new ObjectResult(dtoList);
        }

        //api/CoursePrerequisites/forcourse/id

        /// <summary>
        /// Returns CoursePrerequisites for a specific Course. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/CoursePrerequisites/forcourse/{id}")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetCoursePrerequisitesFor(int? id)
        {
            //return a list of course offerings
            List<CoursePrerequisitesDTO> dtoList = new List<CoursePrerequisitesDTO>();

            var CoursePrerequisites = await _context.CoursePreRequisites
                            .Where(c => c.CourseId.Equals(id))
                            .Include(c => c.Course)
                            .Include(c => c.PreRequisite)
                            .AsNoTracking()
                            .OrderBy(c => c.PreRequisite.CourseCode)
                            .ToListAsync();

            foreach (var Prerequisite in CoursePrerequisites)
            {
                var dtoCoursePrerequisite = new CoursePrerequisitesDTO
                {
                    Id = Prerequisite.Id,
                };

                var CourseDTO = new CourseDTO
                {
                    Id = Prerequisite.Course.Id,
                    Code = Prerequisite.Course.CourseCode,
                    Title = Prerequisite.Course.Title
                };
                dtoCoursePrerequisite.Course = CourseDTO;
                var PrerequisiteDTO = new CourseDTO
                {
                    Id = Prerequisite.PreRequisite.Id,
                    Code = Prerequisite.PreRequisite.CourseCode,
                    Title = Prerequisite.PreRequisite.Title
                };
                dtoCoursePrerequisite.Prerequisite = PrerequisiteDTO;
                dtoList.Add(dtoCoursePrerequisite);
            };
            return new ObjectResult(dtoList);
        }

        //api/CoursePrerequisites/id/isprerequisitefor

        /// <summary>
        /// Returns courses a specific course is a Prerequisites for. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/CoursePrerequisites/{id}/isprerequisitefor")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetCoursePrerequisitesIs(int? id)
        {
            //return a list of course offerings
            List<CoursePrerequisitesDTO> dtoList = new List<CoursePrerequisitesDTO>();

            var CoursePrerequisites = await _context.CoursePreRequisites
                            .Where(c => c.PreRequisiteId.Equals(id))
                            .Include(c => c.Course)
                            .Include(c => c.PreRequisite)
                            .AsNoTracking()
                            .OrderBy(c => c.Course.CourseCode)
                            .ToListAsync();

            foreach (var Prerequisite in CoursePrerequisites)
            {
                var dtoCoursePrerequisite = new CoursePrerequisitesDTO
                {
                    Id = Prerequisite.Id,
                };

                var CourseDTO = new CourseDTO
                {
                    Id = Prerequisite.Course.Id,
                    Code = Prerequisite.Course.CourseCode,
                    Title = Prerequisite.Course.Title
                };
                dtoCoursePrerequisite.Course = CourseDTO;
                var PrerequisiteDTO = new CourseDTO
                {
                    Id = Prerequisite.PreRequisite.Id,
                    Code = Prerequisite.PreRequisite.CourseCode,
                    Title = Prerequisite.PreRequisite.Title
                };
                dtoCoursePrerequisite.Prerequisite = PrerequisiteDTO;
                dtoList.Add(dtoCoursePrerequisite);
            };
            return new ObjectResult(dtoList);
        }
    }
}
