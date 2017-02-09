using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class CoursePreRequisite
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int PreRequisiteId { get; set; }

        public Course Course { get; set; }
        //public Course PreRequisite { get; set; }
    }
}
