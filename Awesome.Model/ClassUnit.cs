using Awesome.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Awesome.Model
{
    public class ClassUnit
   {
        public ClassUnit()
        {
            Schema = new List<Lesson>();
        }

        [Key]
        public int ClassUnitID { get; set; }

        public string ClassName { get; set; }

        public Folder Shared { get; set; }

        public Folder Submission { get; set; }

        public List<Lesson> Schema { get; set; }

        //public ICollection<ApplicationUser> Participants { get; set; }

        public List<Lesson> GetSchema(DateTime _from, DateTime _to)
        {
            return Schema.Where(time => time.StartTime >= _from && time.StopTime <= _to).ToList();
        }
    }
}
