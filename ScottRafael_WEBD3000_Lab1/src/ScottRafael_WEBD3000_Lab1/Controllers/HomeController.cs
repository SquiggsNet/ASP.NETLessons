using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScottRafael_WEBD3000_Lab1.Data;
using ScottRafael_WEBD3000_Lab1.Models.SchoolViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_WEBD3000_Lab1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            IQueryable<EnrollmentDateGroup> data =
                from student in _context.Students
                group student by student.EnrollmentDate into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    StudentCount = dateGroup.Count()
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
