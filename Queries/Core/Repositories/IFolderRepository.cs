using Queries.Core.Domain;
using Queries.Core.Models;
using System.Collections.Generic;

namespace Queries.Core.Repositories
{
    public interface IFolderRepository : IRepository<Folder>
    {
        void AddFileToClassUnitShared(ApplicationUser user, Dossier file, ClassUnit _class = null);
        //IEnumerable<Folder> GetFoldersForClass(int ClassUnitID);
        IEnumerable<Folder> GetSomeFolders(int antal);
        IEnumerable<Folder> GetFoldersByName(string folderName);
        IEnumerable<Dossier> GetAllFilesInFolder(Folder Fold);
    }
}