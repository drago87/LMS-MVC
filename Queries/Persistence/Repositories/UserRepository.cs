using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.ViewModels;
using Queries.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext Ctx;

        //public UserRepository(ApplicationDbContext context) : base(context)
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            Ctx = context;
        }

        #region Users
        public void UpdateUser(ApplicationUser user)
        {
            Ctx.Entry(user).State = EntityState.Modified;
            Ctx.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetUsersByName(string username)
        {
            //IdentityRole a = Ctx.Roles;
            //Ctx.Roles.Where(r=> r.)

            return Ctx.Users
                .Where(u => u.UserName == username);
        }

        public ApplicationUser GetUserById(string id)
        {
            return Ctx.Users.Single(u => u.Id == id);
        }

        //public List<IdentityRole> GetUserRolesNameAsList(ApplicationUser user)
        public List<string> GetUserRolesNameAsList(ApplicationUser user)
        //public RoleViewModel GetMyRoles(ApplicationUser user)
        {
            List<string> rolenames = new List<string>();
            var roles = new List<RoleViewModel>();

            if (user != null && user.Roles.Any())
            {
                var role = new RoleViewModel();
                user.Roles.Select(r => r.RoleId.ToString());
            }
            return rolenames;
        }

        public IEnumerable<ClassUnit> GetClassUnitsFor(ApplicationUser user)
        {
            var  thisuser = Ctx.Users.Include("ClassUnits").Single(u => u.Id == user.Id);
            return thisuser.ClassUnits.ToList();
        }

        public void edit(ApplicationUser user, string roleId, int classunitId)
        {
            var userStore   = new UserStore<ApplicationUser>(Ctx);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var theUser = Ctx.Users
                .SingleOrDefault(b => b.Id == user.Id);

            if (roleId != "-1")
            {
                IdentityRole role = Ctx.Roles.SingleOrDefault(b => b.Id == roleId);
                
                if (GetUserRolesNameAsList(theUser).Contains("Teacher"))
                {
                    userManager.RemoveFromRole(theUser.Id, "Teacher");
                }
                else if (GetUserRolesNameAsList(theUser).Contains("Student"))
                {
                    userManager.RemoveFromRole(theUser.Id, "Student");
                }
                userManager.AddToRole(theUser.Id, role.Name);
            }

            if (classunitId != -1)
            {
                var classunit = Ctx.Classunits.SingleOrDefault(b => b.ClassUnitID == classunitId);

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

                Ctx.Entry(classunit).State = EntityState.Modified;
                Ctx.SaveChanges();
            }
            theUser.Email = user.Email;
            theUser.UserName = user.UserName;
            theUser.PhoneNumber = user.PhoneNumber;

            Ctx.Entry(theUser).State = EntityState.Modified;

            Ctx.SaveChanges();
        }
        #endregion

        #region Roles
        public void AddUserToRole(ApplicationUser user, IdentityRole role)
        {
            var userStore   = new UserStore<ApplicationUser>(Ctx);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.AddToRole(user.Id, role.Name);
            Ctx.SaveChanges();
        }

        public ICollection<IdentityRole> GetAllRoles()
        {
            return Ctx.Roles.ToList();
        }
        #endregion

        //public ApplicationDbContext Ctx
        //{
        //    get { return Ctx as ApplicationDbContext; }
        //}
    }
}
