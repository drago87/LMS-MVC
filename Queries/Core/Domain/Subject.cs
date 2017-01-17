using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Queries.Core.Domain
{
    public class Subject
    {
        [Key]
        public int SubjectID { get; set; }

        [DisplayName("Subject"), Required]
        public string SubjectName { get; set; }
    }
}