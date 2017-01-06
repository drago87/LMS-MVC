using Awesome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesome.Data.Contracts
{
    /// <summary>
    /// Interface for UOW Movie Review
    /// </summary>
    public interface IAwesomeUow
    {
        void Commit();
        IRepository<ClassUnit> Classunits { get; }
        IRepository<Subject> Subjects { get; }
    }
}
