using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Result_Submission_for_Dual_Examinar.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public virtual List<Course> CourseList  { get; set; }
        public virtual List<long> CourseIdList  { get; set; }
    }
}