using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Concentration
    {
        public int Id { get; set; }
        [Required]
        [Remote(action: "VerifyTitle", controller: "Concentrations")]
        public string Title { get; set; }
        public int ProgramId { get; set; }

        public Program Program { get; set; }
        public ICollection<CourseOffering> CourseOfferings { get; set; }
    }
}