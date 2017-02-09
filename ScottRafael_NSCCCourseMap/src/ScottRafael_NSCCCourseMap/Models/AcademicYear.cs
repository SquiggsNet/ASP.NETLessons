using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class AcademicYear
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Semester> Semesters { get; set; }
    }
}
