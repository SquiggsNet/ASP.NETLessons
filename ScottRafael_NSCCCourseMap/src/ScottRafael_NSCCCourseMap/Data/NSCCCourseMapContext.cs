using Microsoft.EntityFrameworkCore;
using ScottRafael_NSCCCourseMap.Models;
using Microsoft.EntityFrameworkCore.Metadata;
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
        public DbSet<ScottRafael_NSCCCourseMap.Models.Program> Programs { get; set; }
        public DbSet<Concentration> Concentrations { get; set; }
        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Course> Courses { get; set;}
        public DbSet<CourseOffering> CourseOfferings { get; set; }
        public DbSet<CoursePreRequisite> CoursePreRequisites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ScottRafael_NSCCCourseMap.Models.Program>().ToTable("Program");
            modelBuilder.Entity<Concentration>().ToTable("Concentration");
            modelBuilder.Entity<AcademicYear>().ToTable("AcademicYear");
            modelBuilder.Entity<Semester>().ToTable("Semester");
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<CourseOffering>().ToTable("CourseOffering");
            modelBuilder.Entity<CoursePreRequisite>().ToTable("CoursePreRequisite");

            modelBuilder.Entity<CoursePreRequisite>()
                .HasOne(c => c.Course)
                .WithMany(c => c.PreRequisites)
                .HasForeignKey(c => c.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CoursePreRequisite>()
                .HasOne(c => c.PreRequisite)
                .WithMany(c => c.IsPreRequisitesFor)
                .HasForeignKey(c => c.PreRequisiteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
