using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Queries.Core.Domain
{
    public class Folder
    {
        [Key]
        public int FolderID { get; set; }

        [DisplayName("Folder Name")]
        public string FolderName { get; set; }

        //[ForeignKey("Files")]
        //public int FileID { get; set; }

        public ClassUnit Clas { get; set; }
        public virtual List<Dossier> Files { get; set; }
    }
}
