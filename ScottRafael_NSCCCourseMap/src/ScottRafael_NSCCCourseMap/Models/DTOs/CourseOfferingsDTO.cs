using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class CourseOfferingsDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public int ConcentrationId { get; set; }
        public string ConcentrationTitle { get; set; }
        public int SemesterId { get; set; }
        public string SemesterName { get; set; }
        public Boolean IsCampusCourse { get; set; }
    }
}
