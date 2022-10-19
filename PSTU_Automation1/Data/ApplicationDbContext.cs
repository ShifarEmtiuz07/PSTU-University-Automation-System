using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PSTU_Automation1.Models;

namespace PSTU_Automation1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       // public DbSet<Student> Student { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Enroll_Course> Enroll_Course { get; set; }
        public DbSet<Undergraduation_Apply> Undergraduation_Apply { get; set; }
        public DbSet<Postgraduation_Apply> Postgraduation_Apply { get; set; }
       
        public object Students { get; internal set; }
       
       
       
       
       
        public DbSet<PSTU_Automation1.Models.a> a { get; set; }
       
        public DbSet<PSTU_Automation1.Models.Student> Student { get; set; }
       
        public DbSet<PSTU_Automation1.Models.StudentProfile> StudentProfile { get; set; }
       
        public DbSet<PSTU_Automation1.Models.Library> Library { get; set; }
       
      
        //public DbSet<PSTU_Automation1.Models.Studentt> Studentt { get; set; }
       
       // public DbSet<PSTU_Automation1.Models.StudentProfile> StudentProfile { get; set; }
    }
}
