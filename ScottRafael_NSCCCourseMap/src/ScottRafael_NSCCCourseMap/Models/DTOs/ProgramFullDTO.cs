using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class ProgramFullDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ConcentrationDTO> Concentrations { get; set; }
    }
}
