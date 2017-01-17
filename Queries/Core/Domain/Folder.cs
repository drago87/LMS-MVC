using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Queries.Core.Domain
{
    public class Folder
    {
        [Key]
        public int FolderID { get; set; }

        [DisplayName("Folder Name"), Required]
        public string FolderName { get; set; }

        //[ForeignKey("Files")]
        //public int FileID { get; set; }

        public ClassUnit ClassUnit { get; set; }

        public virtual List<Dossier> Files { get; set; }

        public Folder()
        {
            Files = new List<Dossier>();
        }
    }
}
