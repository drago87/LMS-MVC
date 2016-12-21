using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LMS.Models
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [DisplayName("Subject")]
        public string SubjectName { get; set; }
    }
}
