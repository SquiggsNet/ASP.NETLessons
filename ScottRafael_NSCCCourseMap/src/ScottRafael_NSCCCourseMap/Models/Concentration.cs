using System.Collections.Generic;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Concentration
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public ICollection<CourseOffering> CourseOfferings { get; set; }
    }
}