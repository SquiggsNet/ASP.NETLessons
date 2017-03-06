using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels
{
    public class CoursePrerequisiteData
    {    
        public ICollection<Course> Courses { get; set; }
        public ICollection<CoursePreRequisite> CoursePreRequisites { get; set; }
        public SelectList CourseList { get; set; }
        public int SelectedCourseID { get; set; }
    }
}
