using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string Title { get; set; }

        public string CourseFull
        {
            get
            {
                return CourseCode + " - " + Title;
            }
        }

        public ICollection<CourseOffering> CourseOfferings { get; set; }

        public ICollection<CoursePreRequisite> CoursePreRequisites { get; set; }

    }
}
