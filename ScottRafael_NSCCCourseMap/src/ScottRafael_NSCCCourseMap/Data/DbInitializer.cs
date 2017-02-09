using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ScottRafael_NSCCCourseMap.Data;

namespace ScottRafael_NSCCCourseMap.Models
{
    public class DbInitializer
    {
        public static void Initialize(NSCCCourseMapContext context)
        {
            context.Database.EnsureCreated();

            // Look for any programs.
            if (context.Programs.Any())
            {
                return;   // DB has been seeded
            }

            //programs
            var programs = new Program[]
            {
                new Program{ Title="Information Technology Diploma" },
                new Program{ Title="Computer Electronics Technician Diploma"}
            };
            foreach (Program p in programs)
            {
                context.Programs.Add(p);
            }
            context.SaveChanges();

            //concentrations
            int itId = context.Programs.Single(p => p.Title == "Information Technology Diploma").Id;
            int cetId = context.Programs.Single(p => p.Title == "Computer Electronics Technician Diploma").Id;
            var concentrations = new Concentration[]
            {
                new Concentration { Title="Common First Semester", ProgramId=itId },
                new Concentration { Title="Application Development Stream", ProgramId=itId },
                new Concentration { Title="Information Systems Management Stream", ProgramId=itId },
                new Concentration { Title="Web Programming Concentration", ProgramId=itId },
                new Concentration { Title="Programming Concentration", ProgramId=itId },
                new Concentration { Title="Database Development Concentration", ProgramId=itId },
                new Concentration { Title="Database Administration Concentration", ProgramId=itId },
                new Concentration { Title="Systems Management Concentration", ProgramId=itId },
                new Concentration { Title="CET Year 1 - Semester 1", ProgramId=cetId },
                new Concentration { Title="CET Year 1 - Semester 2", ProgramId=cetId },
                new Concentration { Title="CET Year 1 - Semester 3", ProgramId=cetId },
                new Concentration { Title="CET Year 2 - Semester 1", ProgramId=cetId },
                new Concentration { Title="CET Year 2 - Semester 2", ProgramId=cetId }
            };
            foreach (Concentration c in concentrations)
            {
                context.Concentrations.Add(c);
            }
            context.SaveChanges();

            //academic years
            var academicYears = new AcademicYear[]
            {
                new AcademicYear{ Title="2016-17" }
            };
            foreach (AcademicYear ay in academicYears)
            {
                context.AcademicYears.Add(ay);
            }
            context.SaveChanges();

            //semesters
            int academicYearId = context.AcademicYears.Single(ay => ay.Title == "2016-17").Id;
            var semesters = new Semester[]
            {
                new Semester { Name="Fall 2016", StartDate=DateTime.Parse("2016-09-06"), EndDate=DateTime.Parse("2016-12-01"), AcademicYearId=academicYearId },
                new Semester { Name="Winter 2017", StartDate=DateTime.Parse("2017-01-04"), EndDate=DateTime.Parse("2017-04-15"), AcademicYearId=academicYearId },
                new Semester { Name="Spring 2017", StartDate=DateTime.Parse("2017-04-15"), EndDate=DateTime.Parse("2017-05-31"), AcademicYearId=academicYearId}
            };
            foreach (Semester s in semesters)
            {
                context.Semesters.Add(s);
            }
            context.SaveChanges();

            //courses
            var courses = new Course[]
            {
                new Course { CourseCode="WEBD 1000", Title="Website Development" },
                new Course { CourseCode="DBAS 1001", Title="Intro to Databases" },
                new Course { CourseCode="PROG 1102", Title="Intro to Programming" },
                new Course { CourseCode="COMM 1100", Title="Technical Communications" },
                new Course { CourseCode="OSYS 1200", Title="Intro to Windows Administration" },
                new Course { CourseCode="NETW 1100", Title="Intro to Networking" },
                new Course { CourseCode="APPD 1001", Title="User Interface Design" },
                new Course { CourseCode="DBAS 1100", Title="Database Development" },
                new Course { CourseCode="SAAD 1001", Title="Intro to Systems Analysis & Design" },
                new Course { CourseCode="PROG 1400", Title="Intro to Object Oriented Programming" },
                new Course { CourseCode="INFT 1300", Title="Human Relations" },
                new Course { CourseCode="OSYS 1000", Title="Operating Systems - UNIX" },
                new Course { CourseCode="HDWR 1100", Title="Hardware" },
                new Course { CourseCode="NETW 1300", Title="Small Network Infrastructure" },
                new Course { CourseCode="SAAD 1002", Title="Intro to Systems Analysis & Design" },
                new Course { CourseCode="NETW 1500", Title="NOS Administration I" },
                new Course { CourseCode="CETN 1000", Title="DC Circuits" },
                new Course { CourseCode="CETN 1005", Title="High Reliability Soldering" },
                new Course { CourseCode="INET 2005", Title="Web Application Development I" },
                new Course { CourseCode="INFT 4000", Title="Special Topics I" },
                new Course { CourseCode="INFT 2000", Title="Professional Development" },
                new Course { CourseCode="INFT 2100", Title="Project Management" },
                new Course { CourseCode="WEBD 3100", Title="Web Design Fundamentals" },
                new Course { CourseCode="APPD 2000", Title="Mobile App Development" },
                new Course { CourseCode="WEBD 3000", Title="Web Application Development II" },
                new Course { CourseCode="INFT 3000", Title="Capstone" },
                new Course { CourseCode="WEBD 2000", Title="Rich Internet Applications" },
                new Course { CourseCode="OSYS 2040", Title="Web Server Fundamentals" },
                new Course { CourseCode="WEBD 3101", Title="Ruby on Rails" },
                new Course { CourseCode="WEBD 3044", Title="Web Content Management Systems" },
                new Course { CourseCode="WEBD 3102", Title="J2EE" },
                new Course { CourseCode="PROG 2100", Title="Programming C++" },
                new Course { CourseCode="PROG 2200", Title="Advanced OOP" },
                new Course { CourseCode="PROG 2400", Title="Data Structures" },
                new Course { CourseCode="PROG 2500", Title="Windows Programming C#" },
                new Course { CourseCode="DBAS 2200", Title="Database Programming" },
                new Course { CourseCode="DBAS 2104", Title="Business Analysis Essentials" },
                new Course { CourseCode="DBAS 3200", Title="Data-Driven Application Programming" },
                new Course { CourseCode="DBAS 2103", Title="Data Provisioning with ETL" },
                new Course { CourseCode="DBAS 2101", Title="Data Reporting" },
                new Course { CourseCode="DBAS 4003", Title="Intro to ABAP" },
                new Course { CourseCode="NETW 2200", Title="Hierarchical Network Infrastructure" },
                new Course { CourseCode="NETW 2500", Title="NOS Administration II" },
                new Course { CourseCode="NETW 1030", Title="NOS Alternate" },
                new Course { CourseCode="DBAS 3040", Title="Database Mgmt & Administration" },
                new Course { CourseCode="NETW 2015", Title="Web Server Administration" },
                new Course { CourseCode="OSYS 2100", Title="Intro to Mainframe Processing" },
                new Course { CourseCode="CSTN 4015", Title="Help Desk & Customer Support" },
                new Course { CourseCode="ICOM 3010", Title="Self Directed Study" },
                new Course { CourseCode="ISEC 3020", Title="Security" },
                new Course { CourseCode="ICOM 3310", Title="Technical Training" },
                new Course { CourseCode="NETW 3200", Title="Microsoft Exchange (Email Systems)" },
                new Course { CourseCode="NETW 3041", Title="Server Virtualization" },
                new Course { CourseCode="DBAS 3060", Title="Data Warehousing Support" },
                new Course { CourseCode="DBAS 3070", Title="Intro to Enterprise Resource Planning" },
                new Course { CourseCode="DBAS 3080", Title="Database Backup and Recovery" },
                new Course { CourseCode="DBAS 2010", Title="Database Design II" },
                new Course { CourseCode="CETN 3010", Title="Programming in C" },
                new Course { CourseCode="CETN 1015", Title="CAD" },
                new Course { CourseCode="CETN 2005", Title="AC Circuits" },
                new Course { CourseCode="CETN 2015", Title="Semiconductor Circuits" },
                new Course { CourseCode="CETN 3001", Title="Embedded Controllers" },
                new Course { CourseCode="CETN 2020", Title="Digital Circuits I" },
                new Course { CourseCode="CETN 2105", Title="Digital Circuits II" },
                new Course { CourseCode="CETN 4001", Title="Senior Project" },
                new Course { CourseCode="CETN 3020", Title="Project Planning" },
                new Course { CourseCode="PROG 3042", Title="Development Process and Controls" }
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            //course prerequisites
            var coursePreRequisites = new CoursePreRequisite[]
            {
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 1015").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "CETN 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2005").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "CETN 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2015").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "CETN 2005").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2020").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "CETN 2015").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2105").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "CETN 2020").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 3001").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "CETN 3010").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 3010").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1102").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "CETN 3020").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "SAAD 1002").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1001").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2010").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2101").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2101").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 2103").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2103").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2104").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1001").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3040").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1001").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3060").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 2010").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3060").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 3040").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3070").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 3040").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3080").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 3040").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 4003").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 4003").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "WEBD 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "ISEC 3020").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1300").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1030").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "OSYS 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1300").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1500").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "OSYS 1200").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2015").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "OSYS 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1300").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2500").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1500").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 3200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1300").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "NETW 3200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1500").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 2040").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1102").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2100").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2100").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "SAAD 1001").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2200").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "SAAD 1001").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2400").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 2100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2500").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2500").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "SAAD 1001").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "SAAD 1001").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1102").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "SAAD 1002").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "NETW 1100").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 2000").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 2000").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "WEBD 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3000").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3044").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3100").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "WEBD 1000").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3101").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id
                },
                new CoursePreRequisite {
                    CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3102").Id,
                    PreRequisiteId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id
                },
            };
            foreach (CoursePreRequisite cpr in coursePreRequisites)
            {
                context.CoursePreRequisites.Add(cpr);
            }
            context.SaveChanges();

            //course offerings
            var courseOfferings = new List<CourseOffering>();

            //FALL 2016
            //COMMON FIRST SEMESTER
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 1, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 1001").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 1, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 1102").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 1, CourseId = context.Courses.Single(c => c.CourseCode == "COMM 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 1, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 1200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 1, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 1, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 1000").Id });

            //CETN COMMON FIRST SEMESTER
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 9, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 1001").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 9, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 1102").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 9, CourseId = context.Courses.Single(c => c.CourseCode == "COMM 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 9, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 1200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 9, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 9, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 1000").Id });

            //WEB PROGRAMMING CONCENTRATION - FALL
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 4000").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "APPD 2000").Id, IsCampusCourse = true });

            //PROGRAMMING CONCENTRATION - FALL
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3102").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "APPD 2000").Id, IsCampusCourse = true });

            //DATA DEV CONCENTRATION - FALL
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "INET 2005").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2104").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "APPD 2000").Id });

            //SYSTEMS CONCENTRATION - FALL
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2500").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1030").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2015").Id, IsCampusCourse = true });

            //DBA CONCENTRATION - FALL
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2500").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 2000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3040").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 2100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id });

            //CETN Semester 4 - FALL
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 12, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 3010").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 12, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 2500").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 12, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 1015").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 12, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2005").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 1, ConcentrationId = 12, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2015").Id });

            //WINTER 2017
            //APP DEV Stream - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "APPD 1001").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "SAAD 1001").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 1400").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 1300").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 1000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 2, CourseId = context.Courses.Single(c => c.CourseCode == "HDWR 1100").Id });

            //SYSTEMS Stream - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 3, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 1300").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 3, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 1000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 3, CourseId = context.Courses.Single(c => c.CourseCode == "HDWR 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 3, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1300").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 3, CourseId = context.Courses.Single(c => c.CourseCode == "SAAD 1002").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 3, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1500").Id });

            //WEB CONCENTRATION - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 3000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 2000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 2040").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3101").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 4, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 3044").Id, IsCampusCourse = true });

            //PROGRAMMING CONCENTRATION - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 3000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "WEBD 2000").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2400").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2500").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 5, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 3042").Id, IsCampusCourse = true });

            //DATA DEV CONCENTRATION - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "PROG 2200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 3000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3200").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2103").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2101").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 6, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 4003").Id, IsCampusCourse = true });

            //SYSTEMS CONCENTRATION - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "CSTN 4015").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "ICOM 3010").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "ISEC 3020").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "ICOM 3310").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 3200").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 8, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 3041").Id, IsCampusCourse = true });

            //DBA CONCENTRATION - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3060").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3070").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "ISEC 3020").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 3080").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 3200").Id, IsCampusCourse = true });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 7, CourseId = context.Courses.Single(c => c.CourseCode == "DBAS 2010").Id });

            //CETN Semester 2 - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 10, CourseId = context.Courses.Single(c => c.CourseCode == "INFT 1300").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 10, CourseId = context.Courses.Single(c => c.CourseCode == "OSYS 1000").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 10, CourseId = context.Courses.Single(c => c.CourseCode == "HDWR 1100").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 10, CourseId = context.Courses.Single(c => c.CourseCode == "SAAD 1002").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 10, CourseId = context.Courses.Single(c => c.CourseCode == "NETW 1500").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 10, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 1000").Id });

            //CETN Semester 5 - WINTER
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 13, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 3001").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 13, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2020").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 13, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 2105").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 13, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 4001").Id });
            courseOfferings.Add(new CourseOffering { SemesterId = 2, ConcentrationId = 13, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 3020").Id });

            //CETN Semester 3 - SPRING
            courseOfferings.Add(new CourseOffering { SemesterId = 3, ConcentrationId = 11, CourseId = context.Courses.Single(c => c.CourseCode == "CETN 1005").Id });

            foreach (CourseOffering co in courseOfferings)
            {
                context.CourseOfferings.Add(co);
            }
            context.SaveChanges();
        }
    }
}
