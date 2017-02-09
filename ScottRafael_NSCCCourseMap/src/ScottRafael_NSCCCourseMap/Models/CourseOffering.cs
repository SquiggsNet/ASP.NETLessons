using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class CourseOffering
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ConcentrationId { get; set; }
        public int SemesterId { get; set; }
        public bool IsCampusCourse { get; set; }

        public Course Course { get; set; }
        public Concentration Concentration { get; set; }
        public Semester Semester { get; set; }
    }
}
