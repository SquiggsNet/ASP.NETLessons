using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class SemesterFullDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AcademicYear {get;set;}
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CourseOfferingsDTO> CourseOfferings { get; set; }
    }
}
