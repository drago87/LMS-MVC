using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Queries.Core.Models;
using Queries.Core.Domain;

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

        #region Users
        public ICollection<ApplicationUser> GetAllUsers()
        {
            return _ctx.Users.ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            var temp = _ctx.Users.FirstOrDefault(b => b.Id == id);
            return temp;
        }

        public void UpdateUser(ApplicationUser user)
        {
            _ctx.Entry(user).State = EntityState.Modified;

            _ctx.SaveChanges();
        }

        #endregion

        #region Roles
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

        #endregion

        #region Classes
        public ICollection<ClassUnit> GetAllClasses()
        {
            return _ctx.Classunits.ToList();
        }

        public ClassUnit GetClassUnitByID(int ClassId)
        {
            return _ctx.Classunits.FirstOrDefault(b => b.ClassUnitID == ClassId);
        }

        public void AddUserToClass(ApplicationUser user, ClassUnit classunit)
        {

            var temp = _ctx.Classunits.Single(b => b.ClassUnitID == classunit.ClassUnitID);//.Participants.Add(user);
            var temp2 = _ctx.Users.Single(b => b.Id == user.Id);

            temp.Participants.Add(temp2);


        }

        public void UpdateClassUnit(ClassUnit classunit)
        {
            _ctx.Entry(classunit).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ClassUnit RemoveClassUnit(int id)
        {
            ClassUnit classunit = _ctx.Classunits.Find(id);
            if (classunit == null)
            {
                //return NotFound();
                return null;
            }

            _ctx.Classunits.Remove(classunit);
            _ctx.SaveChanges();
            return classunit;

        }

        internal object AddClassUnit(ClassUnit classunit)
        {
            ClassUnit newClassunit = _ctx.Classunits.Add(classunit);
            _ctx.SaveChanges();
            return newClassunit.ClassUnitID;
        }
        #endregion

        #region Subject
        public ICollection<Subject> GetAllSubjects()
        {
            return _ctx.Subjects.ToList();
        }

        public Subject GetSubjectByName(string Name)
        {
            return _ctx.Subjects.FirstOrDefault(b => b.SubjectName == Name);
        }


        #endregion

        #region Folders
        public ICollection<Folder> GetAllFolders()
        {
            return _ctx.Folders.ToList();
        }

        public Folder GetFolderByID(int ID)
        {
            return _ctx.Folders.FirstOrDefault(b => b.FolderID == ID);
        }

        #endregion

        #region Files

        public ICollection<Dossier> GetAllFiles()
        {
            return _ctx.Dossiers.ToList();
        }

        public ICollection<Dossier> GetAllFilesInFolder(Folder Fold)
        {

            return _ctx.Dossiers.Where(b => b.Folder == Fold).ToList();
        }

        #endregion


        public void edit(ApplicationUser user, string roleId, int classunitId)
        {
            var userStore = new UserStore<ApplicationUser>(_ctx);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser theUser = _ctx.Users.SingleOrDefault(b => b.Id == user.Id);

            if (roleId != "-1")
            {
                IdentityRole role = _ctx.Roles.SingleOrDefault(b => b.Id == roleId);
                //AddRemoveFromRole
                if (GetUserRolesNameAsList(theUser).Contains(role.Name))
                {
                    userManager.RemoveFromRole(theUser.Id, role.Name);
                }
                else
                {
                    userManager.AddToRole(theUser.Id, role.Name);
                }
            }

            if (classunitId != -1)
            {
                ClassUnit classunit = _ctx.Classunits.SingleOrDefault(b => b.ClassUnitID == classunitId);


                //AddRemoveFromClassUnit
                if (theUser.ClassUnits.Contains(classunit))
                {
                    //theUser.ClassUnits.Remove(classunit);
                    classunit.Participants.Remove(theUser);
                }
                else
                {
                    //theUser.ClassUnits.Add(classunit);
                    classunit.Participants.Add(theUser);
                }

                _ctx.Entry(classunit).State = EntityState.Modified;
                _ctx.SaveChanges();
                
            }

            theUser.Email = user.Email;
            theUser.UserName = user.UserName;
            theUser.PhoneNumber = user.PhoneNumber;

            _ctx.Entry(theUser).State = EntityState.Modified;

            _ctx.SaveChanges();
        }

        public List<string> GetUserRolesNameAsList(ApplicationUser applicationUser)
        {
            List<string> tempRoleNames = new List<string>();

            var temp = _ctx.Users.Where(x => x.Id == applicationUser.Id).Include(z => z.Roles).First();

            if (applicationUser.Roles.Any())
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

        public List<string> GetUserClassUnitsNameAsList(ApplicationUser applicationUser)
        {
            List<string> tempClassNames = new List<string>();
            //Needed dont ask my why
            var temp = _ctx.Users.Where(x => x.Id == applicationUser.Id).Include(z => z.ClassUnits).First();


            
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