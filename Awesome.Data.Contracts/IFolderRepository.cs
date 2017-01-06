using Awesome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesome.Data.Contracts
{
    public interface IFolderRepository : IRepository<Folder>
    {
        IEnumerable<Folder> GetSomeFolders(int antal);
        //IEnumerable<Folder> GetFoldersForClass(int ClassUnitID);
    }
}
