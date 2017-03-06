using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels
{
    public class CourseInformation
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public ICollection<CoursePreRequisite> PreRequisiteFor { get; set; }
        public ICollection<CoursePreRequisite> CoursePreRequisites { get; set; }

    }
}
