using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.NSCCCourseMapViewModels
{
    public class CourseOfferingIndexData
    {
        public ICollection<Course> Courses { get; set; }
        public ICollection<Concentration> Concentrations { get; set; }
        public ICollection<CourseOffering> CourseOfferings { get; set; }
        public ICollection<Semester> Semesters { get; set; }

        public SelectList SemesterList { get; set; }
        public int SelectedSemesterID { get; set; }

        public SelectList ConcentrationList { get; set; }
        public int SelectedConcentrationID { get; set; }
    }
}
