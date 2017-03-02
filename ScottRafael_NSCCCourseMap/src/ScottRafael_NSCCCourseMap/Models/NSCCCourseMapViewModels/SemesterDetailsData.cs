using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels
{
    public class SemesterCourseDetailsData
    {
        public string CourseCode { get; set; }
        public string CourseTitle { get; set; }
        public string Concentration { get; set; }
        [Display(Name = "Campus Course")]
        public bool CampusCourse { get; set; }

        
        public string CourseFull
        {
            get
            {
                return CourseCode + " - " + CourseTitle;
            }
        }
    }
}
