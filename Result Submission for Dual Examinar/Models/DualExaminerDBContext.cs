using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Result_Submission_for_Dual_Examinar.Models
{
    public class DualExaminerDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
    }
}