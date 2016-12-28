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
                var folder = _ctx.MyFolders.Find(user.ClassUnits.FirstOrDefault().Shared.FolderID);
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

        public void AddUserToClass(ApplicationUser user, ClassUnit classunit)
        {
            
            var temp = _ctx.MyClassUnit.Single(b => b.ClassUnitID == classunit.ClassUnitID);//.Participants.Add(user);
            var temp2 = _ctx.Users.Single(b => b.Id == user.Id);
            /*if (temp2.ClassUnits == null)
            {
                temp2.ClassUnits = new List<ClassUnit>();
            }
            if (temp.Participants == null)
            {
                temp.Participants = new List<ApplicationUser>();
            }*/
            
            temp.Participants.Add(temp2);
            //classunit.Participants.Add(user);
            
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

        #region ClassUnits

        internal ICollection<ClassUnit> GetClassUnits()
        {
            var classunits = _ctx.MyClassUnit.ToList();
            return classunits;
        }

        public void UpdateClassUnit(ClassUnit classunit)
        {
            _ctx.Entry(classunit).State = EntityState.Modified;
            _ctx.SaveChanges(); 
        }

        //public Person RemovePerson(int id)
        public ClassUnit RemoveClassUnit(int id)
        {
            ClassUnit classunit = _ctx.MyClassUnit.Find(id);
            if (classunit == null)
            {
                //return NotFound();
                return null;
            }

            _ctx.MyClassUnit.Remove(classunit);
            _ctx.SaveChanges();
            return classunit;

        }

        internal object AddClassUnit(ClassUnit classunit)
        {
            ClassUnit newClassunit = _ctx.MyClassUnit.Add(classunit);
            _ctx.SaveChanges();
            return newClassunit.ClassUnitID;
        }

        #endregion

        #region Students

        //internal ICollection<user> GetStudents()
        //{
        //    var classunits = _ctx.my.ToList();
        //    return classunits;
        //}

        #endregion
        public void edit(ApplicationUser user, string roleId, int classunitId)
        {
            IdentityRole role = _ctx.Roles.SingleOrDefault(b => b.Id == roleId);
            ClassUnit classunit = _ctx.MyClassUnit.SingleOrDefault(b => b.ClassUnitID == classunitId);

            var userStore = new UserStore<ApplicationUser>(_ctx);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser theUser = _ctx.Users.SingleOrDefault(b => b.Id == user.Id);

            theUser.ClassUnits.Add(classunit);
            classunit.Participants.Add(theUser);
            
            userManager.AddToRole(theUser.Id, role.Name);

            //_ctx.Entry(user).State = EntityState.Modified;

            _ctx.SaveChanges();
        }

        public List<string> ReturnUserRolesNameAsList(ApplicationUser applicationUser)
        {
            List<string> tempRoleNames = new List<string>();
            
            if (applicationUser.Roles != null && applicationUser.Roles.Count > 0)
            {
                foreach (var item in applicationUser.Roles)
                {
                    tempRoleNames.Add(GetRoleById(item.RoleId).Name);
                }
            }
            else
            {
                tempRoleNames.Add("Not assigned a Role yet!");
            }

            return tempRoleNames;
        }

        public List<string> ReturnUserClassUnitsNameAsList(ApplicationUser applicationUser)
        {
            List<string> tempClassNames = new List<string>();
            

            if (applicationUser.ClassUnits != null && applicationUser.ClassUnits.Count > 0)
            {
                foreach (var item in applicationUser.ClassUnits)
                {
                    tempClassNames.Add(item.ClassName);
                }
            }
            else
            {
                tempClassNames.Add("Not assigned a Class unit yet!");
            }

            return tempClassNames;
        }
 
    }
}