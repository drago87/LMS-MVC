using LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LMS.Models
{
    public class ClassUnit
    {
        [Key]
        public int ClassUnitID { get; set; }

        public string ClassName { get; set; }

        public ICollection<ApplicationUser> Participants { get; set; }

        public Folder Shared { get; set; }

        public Folder Submission { get; set; }

    }
}
