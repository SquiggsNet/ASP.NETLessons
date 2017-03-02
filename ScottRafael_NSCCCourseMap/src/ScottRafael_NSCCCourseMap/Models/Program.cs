using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class Program
    {
        public int Id { get; set; }

        [Required]
        [Remote(action: "VerifyTitle", controller: "Programs")]
        public string Title { get; set; }

        public ICollection<Concentration> Concentrations { get; set; }
    }
}
