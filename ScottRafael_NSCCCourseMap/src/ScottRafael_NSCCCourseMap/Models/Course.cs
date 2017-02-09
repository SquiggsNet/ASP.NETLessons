using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }

        public ICollection<CourseOffering> CourseOfferings { get; set; }
        public ICollection<CoursePreRequisite> CoursePreRequisites { get; set; }
    }
}
