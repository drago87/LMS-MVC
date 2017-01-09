using Queries.Core.Domain;
using Queries.Core.Models;

namespace Queries.Core.Repositories
{
    public interface IFolderRepository : IRepository<Folder>
    {
        //IEnumerable<Folder> GetSomeFolders(int antal);
        //IEnumerable<Folder> GetFoldersForClass(int ClassUnitID);
        //void AddFileToClassUnitShared(ApplicationUser user, Dossier file, ClassUnit _class = null);
    }
}