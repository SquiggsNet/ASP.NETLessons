using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Program
    {

        public int ID { get; set; }
        public string Title { get; set; }

        public ICollection<Concentration> Concentrations { get; set; }
    }
}
