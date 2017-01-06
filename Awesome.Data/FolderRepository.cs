using Awesome.Data.Contracts;
using Awesome.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesome.Data
{
    class FolderRepository : EFRepository<Folder>, IFolderRepository
    {
        public FolderRepository(AwesomeDbContext context) : base(context) { }

        public IEnumerable<Folder> GetSomeFolders(int pageSize)
        {
            return DbContext.Folders
             //.Include(f => f.)
             //.OrderBy(f => f.)
             //.Skip((pageIndex - 1) * pageSize)
             .Take(pageSize)
             //.ToList()
             ;
        }

        //public IEnumerable<Folder> GetFoldersByName(string folderName)
        //{
        //    return DbContext.Folders
        //        .Where(f => f.FolderName == folderName);
        //}

        public AwesomeDbContext DbContext
        {
            get { return DbContext as AwesomeDbContext; }
        }
    }
}
