using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class CoursePrerequisitesDTO
    {
        public int Id { get; set; }
        public CourseDTO Course { get; set; }
        public CourseDTO Prerequisite { get; set; }
    }
}
