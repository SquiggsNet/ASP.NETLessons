using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        [Remote(action: "VerifyCourseCode", controller: "Courses")]
        public string CourseCode { get; set; }
        [Required]
        //[Remote(action: "VerifyTitle", controller: "Courses")]
        public string Title { get; set; }

        public string CourseFull
        {
            get
            {
                return CourseCode + " - " + Title;
            }
        }

        public ICollection<CourseOffering> CourseOfferings { get; set; }
        public ICollection<CoursePreRequisite> PreRequisites { get; set; }
        public ICollection<CoursePreRequisite> IsPreRequisitesFor { get; set; }

    }
}
