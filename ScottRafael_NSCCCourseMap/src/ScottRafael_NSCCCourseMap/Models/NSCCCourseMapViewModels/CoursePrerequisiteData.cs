using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels
{
    public class CoursePrerequisiteData
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public Course PrerequisiteCourse { get; set; }
    }
}
