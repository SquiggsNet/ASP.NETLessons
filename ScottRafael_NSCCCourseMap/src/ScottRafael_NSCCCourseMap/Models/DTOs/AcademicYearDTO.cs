﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Models.DTOs
{
    public class AcademicYearDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<SemesterDTO> Semesters { get; set; }
    }
}
