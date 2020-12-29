using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Result_Submission_for_Dual_Examinar.Models
{
    public class StudentCourse
    {
        public long StudentCourseId{ get; set; }
        public long StudentId { get; set; }
        public virtual User Student{ get; set; }
        public long CourseId { get; set; }
        public virtual Course Course{ get; set; }
    }
}