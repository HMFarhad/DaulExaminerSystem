using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Result_Submission_for_Dual_Examinar.Models
{
    public class Mark
    {
        public long MarkId { get; set; }
        public virtual User Student { get; set; }
        public long StudentId { get; set; }
        public virtual Course Course { get; set; }
        public long CourseId { get; set; }
        [Range(0, 40, ErrorMessage =" Class Test marks should be between 0 to 40! ")]
        public decimal MidCTMark{ get; set; }
        [Range(0, 60, ErrorMessage = " Final exam marks should be between 0 to 60! ")]
        public decimal MidExamMark{ get; set; }
        [Range(0, 40, ErrorMessage = " Class Test marks should be between 0 to 40! ")]
        public decimal FinalCTMark { get; set; }
        [Range(0, 60, ErrorMessage = " Final exam marks should be between 0 to 60! ")]
        public decimal FinalExamMark { get; set; }
        [NotMapped]
        public decimal Result { get; set; }
    }
}