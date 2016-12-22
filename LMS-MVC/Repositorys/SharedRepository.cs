using LMS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMS_MVC.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LMS_MVC.Repositorys
{
    public class SharedRepository
    {
        private ApplicationDbContext _ctx;
        //private ApplicationUserManager _userManager;
        

        public SharedRepository()
        {
            
            _ctx = new ApplicationDbContext();
        }

        public void AddFileToClassUnitShared(ApplicationUser user, Dossier file, ClassUnit _class = null)
        {

            if (_class == null)
            {
                var folder = _ctx.MyFolders.Find(user.Classunit.FirstOrDefault().Shared.FolderID);
                file.Folder = folder;
                folder.Files.Add(file);
            }
            else
            {
                var folder = _ctx.MyFolders.Find(_ctx.MyClassUnit.Find(_class.ClassUnitID).Shared.FolderID);
                file.Folder = folder;
                folder.Files.Add(file);
            }
            _ctx.SaveChanges();

        }
        public ICollection<ApplicationUser> GetAllUsers()
        {
            return _ctx.Users.ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _ctx.Users.FirstOrDefault(b => b.Id == id);
        }

        public void UpdateUser(ApplicationUser user)
        {
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ICollection<IdentityRole> GetAllRoles()
        {
            return _ctx.Roles.ToList();
        }

        public IdentityRole GetRoleById(string id)
        {
            return _ctx.Roles.FirstOrDefault(b => b.Id == id);
        }

        public void AddUserToRole(ApplicationUser user, IdentityRole role)
        {
            var userStore = new UserStore<ApplicationUser>(_ctx);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.AddToRole(user.Id, role.Name);
            _ctx.SaveChanges();
        }

        public ICollection<ClassUnit> GetAllClasses()
        {
           return _ctx.MyClassUnit.ToList();
        }

        public ClassUnit GetClassUnitByID(int ClassId)
        {
            return _ctx.MyClassUnit.FirstOrDefault(b => b.ClassUnitID == ClassId);
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