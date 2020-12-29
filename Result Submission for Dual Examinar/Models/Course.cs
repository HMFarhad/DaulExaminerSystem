using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Result_Submission_for_Dual_Examinar.Models
{
    public class Course
    {
        public long CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long MidTeacherId { get; set; }
        public long FinalTeacherId { get; set; }
        [NotMapped]
        public virtual User MidTeacher { get; set; }
        [NotMapped]
        public virtual User FinalTeacher { get; set; }

    }
}