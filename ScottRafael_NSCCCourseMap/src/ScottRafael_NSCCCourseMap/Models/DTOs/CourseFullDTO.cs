using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class CourseFullDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public List<CourseDTO> PreRequisites { get; set; }
        public List<CourseDTO> IsPreRequisitesFor { get; set; }
    }
}
