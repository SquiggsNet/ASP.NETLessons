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
    public class ConcentrationsController : Controller
    {
        private readonly NSCCCourseMapContext _context;

        public ConcentrationsController(NSCCCourseMapContext context)
        {
            _context = context;    
        }

        // GET: Concentrations
        public async Task<IActionResult> Index()
        {
            var Concentrations = _context.Concentrations.Include(c => c.Program);
            return View(await Concentrations.ToListAsync());
        }

        // GET: Concentrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concentration = await _context.Concentrations
                .Include(c => c.Program)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (concentration == null)
            {
                return NotFound();
            }

            return View(concentration);
        }

        // GET: Concentrations/Create
        public IActionResult Create()
        {
            //ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id");
            PopulateProgramsDropDownList();
            return View();
        }

        // POST: Concentrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProgramId,Title")] Concentration concentration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(concentration);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id", concentration.ProgramId);
            PopulateProgramsDropDownList(concentration.ProgramId);
            return View(concentration);
        }

        // GET: Concentrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var concentration = await _context.Concentrations.SingleOrDefaultAsync(m => m.Id == id);
            var concentration = await _context.Concentrations.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            if (concentration == null)
            {
                return NotFound();
            }
            //ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id", concentration.ProgramId);
            PopulateProgramsDropDownList(concentration.ProgramId);
            return View(concentration);
        }

        // POST: Concentrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concentrationToUpdate = await _context.Concentrations
                .SingleOrDefaultAsync(c => c.Id == id);

            if (await TryUpdateModelAsync<Concentration>(concentrationToUpdate,
                "",
                c => c.ProgramId, c => c.Title))
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction("Index");
            }
            //ViewData["ProgramId"] = new SelectList(_context.Programs, "Id", "Id", concentration.ProgramId);
            PopulateProgramsDropDownList(concentrationToUpdate.ProgramId);
            return View(concentrationToUpdate);
        }

        private void PopulateProgramsDropDownList(object selectedProgram = null)
        {
            var programsQuery = from p in _context.Programs
                                   orderby p.Title
                                   select p;
            ViewBag.ProgramId = new SelectList(programsQuery.AsNoTracking(), "Id", "Title", selectedProgram);
        }

        // GET: Concentrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concentration = await _context.Concentrations
                .Include(c => c.Program)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.Id == id);
            if (concentration == null)
            {
                return NotFound();
            }

            return View(concentration);
        }

        // POST: Concentrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concentration = await _context.Concentrations.SingleOrDefaultAsync(m => m.Id == id);
            _context.Concentrations.Remove(concentration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ConcentrationExists(int id)
        {
            return _context.Concentrations.Any(e => e.Id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyTitle(string title)
        {
            if (_context.Concentrations.Any(c => c.Title == title))
            {
                return Json(data: $"The title {title} is already in use.");
            }

            return Json(data: true);
        }

        //API Methods

        //api/Concentrations

        /// <summary>
        /// Returns a collection of Concentrations.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Concentrations")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetConcentrations()
        {
            //return a list of concentrations
            List<ConcentrationDTO> dtoList = new List<ConcentrationDTO>();

            var Concentrations = await _context.Concentrations
                        .Include(c => c.Program)
                        .OrderBy(c => c.Title)
                        .AsNoTracking()
                        .ToListAsync();

            foreach (var concetration in Concentrations)
            {
                var dto = new ConcentrationDTO
                {
                    Id = concetration.Id,
                    Title = concetration.Title,
                    CollegeProgram = concetration.Program.Title,
                };
                dtoList.Add(dto);
            }
                        
            return new ObjectResult(dtoList);
        }

        //api/Concentrations/id

        /// <summary>
        /// Returns a specific Concentration. 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Concentrations/{id}")]
        [EnableCors("AllowAllOrigins")]
        public async Task<IActionResult> GetConcentration(int? id)
        {
            var Concentration = await _context.Concentrations
                        .Include(c => c.Program)
                        .AsNoTracking()
                        .SingleOrDefaultAsync(m => m.Id == id);

            if (Concentration == null)
            {
                return NotFound();
            }

            ConcentrationDTO dtoConcentration = new ConcentrationDTO {
                    Id = Concentration.Id,
                    Title = Concentration.Title,
                    CollegeProgram = Concentration.Program.Title,
            };

           

            return new ObjectResult(dtoConcentration);
        }
    }
}
