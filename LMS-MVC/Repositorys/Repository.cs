using LMS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace LMS_MVC.Repositorys
{
    public class Repository
    {
        private ApplicationDbContext _ctx;

        public Repository()
        {
            _ctx = new ApplicationDbContext();
        }

        public ICollection<ClassUnit> GetAllClasses()
        {
            return _ctx.MyClassUnit.ToList();
        }

        public ClassUnit GetClassUnitByID(string ClassName)
        {
            return _ctx.MyClassUnit.FirstOrDefault(b => b.ClassName == ClassName);
        }

        public ICollection<Subject> GetAllSubjects()
        {
            return _ctx.MySubjects.ToList();
        }

        public Subject GetSubjectByName(string Name)
        {
            return _ctx.MySubjects.FirstOrDefault(b => b.SubjectName == Name);
        }

        public ICollection<Folder> GetAllFolders()
        {
            return _ctx.MyFolders.ToList();
        }

        public Folder GetFolderByID(int ID)
        {
            return _ctx.MyFolders.FirstOrDefault(b => b.FolderID == ID);
        }

        public ICollection<Dossier> GetAllFiles()
        {
            return _ctx.MyFiles.ToList();
        }

        public ICollection<Dossier> GetAllFilesInFolder(Folder Fold)
        {
            return _ctx.MyFiles.Where(b => b.Folder == Fold).ToList();
        }
    }
}