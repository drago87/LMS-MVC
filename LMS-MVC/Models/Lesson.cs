using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }

        [DisplayName("Lesson Start")]
        public DateTime StartTime { get; set; }

        [DisplayName("Lesson Done")]
        public DateTime StopTime { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        [ForeignKey("ClassUnit")]
        public int ClassUnitID { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ClassUnit ClassUnit { get; set; }
    }
}