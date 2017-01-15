using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_WEBD3000_Lab1.Models.SchoolViewModels
{
    public class InstructorIndexData
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
