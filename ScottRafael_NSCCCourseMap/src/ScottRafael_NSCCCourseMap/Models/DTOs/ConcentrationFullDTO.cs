using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class ConcentrationFullDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CollegeProgram { get; set; }
        public List<CourseOfferingsDTO> CourseOfferings { get; set; }
    }
}
