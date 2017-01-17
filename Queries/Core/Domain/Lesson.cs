using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Queries.Core.Domain
{
    public class Lesson
    {
        [Key]
        public int LessonID { get; set; }

        [DisplayName("Lesson Start")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DisplayName("Lesson Done")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StopTime { get; set; }

        [ForeignKey("Subject")]
        public int SubjectID { get; set; }

        [ForeignKey("ClassUnit")]
        public int ClassUnitID { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual ClassUnit ClassUnit { get; set; }
    }
}