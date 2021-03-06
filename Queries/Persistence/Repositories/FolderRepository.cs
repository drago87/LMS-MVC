﻿using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Queries.Core.Repositories
{
    public class FolderRepository : Repository<Folder>, IFolderRepository
    {
        private readonly ApplicationDbContext Ctx;

        public FolderRepository(ApplicationDbContext context) : base(context)
        {
            Ctx = context;
        }

        public void AddFileToClassUnitShared(ApplicationUser user, Dossier file, ClassUnit _class = null)
        {
            if (_class == null)
            {
                /*var folder = Ctx.Folders.Find(user.ClassUnits.FirstOrDefault().Shared.FolderID);
                file.Folder = folder;
                folder.Files.Add(file);*/
            }
            else
            {
                //var MyClassUnit = user.ClassUnits;
                //var folder = Ctx.Folders.Find(MyClassUnit.Find(_class.ClassUnitID).Shared.FolderID);
                //file.Folder = folder;
                //folder.Files.Add(file);
            }
            Ctx.SaveChanges();
        }

        public IEnumerable<Folder> GetSomeFolders(int pageSize)
        {
            return Ctx.Folders
                //.Include(f => f.)
                //.OrderBy(f => f.)
                //.Skip((pageIndex - 1) * pageSize)
             .Take(pageSize)
                //.ToList()
             ;
        }

        public IEnumerable<Folder> GetFoldersByName(string folderName)
        {
            return Ctx.Folders
                .Where(f => f.FolderName == folderName);
        }

        public IEnumerable<Dossier> GetAllFilesInFolder(Folder folder)
        {
            return Ctx.Dossiers.Where(d => d.Folder == folder).ToList();
        }

        //public ApplicationDbContext Ctx
        //{
        //    get { return Ctx as ApplicationDbContext; }
        //}
    }
}
