using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScottRafael_NSCCCourseMap.Data
{
    public class NSCCCourseMapContext : DbContext
    {
        public NSCCCourseMapContext(DbContextOptions<NSCCCourseMapContext> options) : base(options)
        {
        }

        //define collections /dbsets
    }
}
