using System.Collections.Generic;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Concentration
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProgramId { get; set; }

        public Program Program { get; set; }
        public ICollection<CourseOffering> CourseOfferings { get; set; }
    }
}