using System;
using System.ComponentModel.DataAnnotations;

namespace ScottRafael_WEBD3000_Lab1.Models.SchoolViewModels
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}
