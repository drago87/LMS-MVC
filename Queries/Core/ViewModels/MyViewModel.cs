using Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.ViewModels
{
    public class MyViewModel
    {
        public List<ClassUnit> ClassUnits { get; set; }

        public List<FileViewModel> Shared { get; set; }

        public List<FileViewModel> Submission { get; set; }
    }
}
