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

        public ClassUnit GetClassUnitByID(string ID)
        {
            return null;
        }

        public ICollection<Subject> GetAllSubjects()
        {
            return _ctx.MySubjects.ToList();
        }

        public Subject GetSubjectByID(string ID)
        {
            return null;
        }

        public ICollection<Folder> GetAllFolders()
        {
            return _ctx.MyFolders.ToList();
        }

        public Folder GetFolderByID(string ID)
        {
            return null;
        }
    }
}