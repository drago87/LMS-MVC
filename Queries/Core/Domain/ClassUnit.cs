﻿using Queries.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Queries.Core.Domain
{
    public class ClassUnit
   {
        [Key]
        public int ClassUnitID { get; set; }

        [Required]
        [DisplayName("Class Name")]
        public string ClassName { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; }

        public virtual List<Folder> Folders { get; set; }

        //public Folder Shared { get; set; }
        //public Folder Submission { get; set; }

        public List<Lesson> Schema { get; set; }

        public ClassUnit()
        {
            Schema = new List<Lesson>();
            Participants = new List<ApplicationUser>();
            Folders = new List<Folder>();
        }

        public List<Lesson> GetSchema(DateTime _from, DateTime _to)
        {
            return Schema.Where(time => time.StartTime >= _from && time.StopTime <= _to).ToList();
        }
    }
}
