using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LMS.Models
{
    public class Files
    {
        [Key]
        public int FileID { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        [DisplayName("File Path")]
        public string FilePath { get; set; }

        [Required]
        public Folder Folder { get; set; }

    }
}
