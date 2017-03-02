using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class CourseOffering
    {
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int ConcentrationId { get; set; }
        [Required]
        public int SemesterId { get; set; }
        public bool IsCampusCourse { get; set; }

        public Course Course { get; set; }
        public Concentration Concentration { get; set; }
        public Semester Semester { get; set; }
    }
}
