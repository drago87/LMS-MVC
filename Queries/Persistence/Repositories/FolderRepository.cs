//using Queries.Core.Domain;
//using Queries.Core.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Queries.Core.Repositories
//{
//    public class FolderRepository : IFolderRepository
//    {
//        //public FolderRepository(ApplicationDbContext context) : base(context)
//        //public FolderRepository() : base() { }
//        //{ }

//        public void AddFileToClassUnitShared(ApplicationUser user, Dossier file, ClassUnit _class = null)
//        {
//            if (_class == null)
//            {
//                //var folder = Ctx.Folders.Find(user.ClassUnits.FirstOrDefault().Shared.FolderID);
//                //file.Folder = folder;
//                //folder.Files.Add(file);
//            }
//            else
//            {
//                //var MyClassUnit = 
//                //var folder = Ctx.Folders.Find(MyClassUnit.Find(_class.ClassUnitID).Shared.FolderID);
//                //file.Folder = folder;
//                //folder.Files.Add(file);
//            }
//            Ctx.SaveChanges();
//        }

//        public ApplicationDbContext Ctx
//        {
//            get { return Ctx as ApplicationDbContext; }
//        }
//    }
//}
