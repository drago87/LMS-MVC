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
            return Ctx.Users
                .Where(u => u.UserName == username);
        }

        public ApplicationUser GetUserById(string id)
        {
            return Ctx.Users.Single(u => u.Id == id);
        }

        public IEnumerable<ClassUnit> GetClassUnitsFor(ApplicationUser user)
        {
            var  thisuser = Ctx.Users.Include("ClassUnits").Single(u => u.Id == user.Id);
            return thisuser.ClassUnits.ToList();
        }

        public IEnumerable<IdentityRole> GetRolesFor(ApplicationUser user)
        {
            var iroles = new List<IdentityRole>();
            var uroles = UserWithRoles(user).Roles.ToList();

            uroles.ForEach(ur =>
            {
                IdentityRole irole = Ctx.Roles.SingleOrDefault(r => r.Id == ur.RoleId);
                iroles.Add(irole);
            });

            return iroles;
        }

        public void edit(ApplicationUser user, string roleId, int classunitId)
        {
            var userStore = new UserStore<ApplicationUser>(Ctx);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var theUser = Ctx.Users.SingleOrDefault(u => u.Id == user.Id);

            editRole(user, roleId, userManager);
            editClassunit(user, classunitId);

            theUser.Email = user.Email;
            theUser.UserName = user.UserName;
            theUser.PhoneNumber = user.PhoneNumber;

            Ctx.Entry(theUser).State = EntityState.Modified;

            Ctx.SaveChanges();
        }

        public void editRole(ApplicationUser user, string roleId, UserManager<ApplicationUser> userManager)
        {
            // if (roleId != "-1") {
            //var role = GetRoleById(roleId);
            var role = Ctx.Roles.Find(roleId);

            if (HasRole(user, "Teacher"))
            {
                userManager.RemoveFromRole(user.Id, "Teacher");
            }

            if (HasRole(user, "Student"))
            {
                userManager.RemoveFromRole(user.Id, "Student");
            }

            userManager.AddToRole(user.Id, role.Name);
            //}
        }

        public void editClassunit(ApplicationUser user, int classunitId)
        {
            //if (classunitId != -1) {
                //var classunit = Ctx.Classunits.SingleOrDefault(c => c.ClassUnitID == classunitId);
                var classunit = Ctx.Classunits.Find(classunitId);

                if (user.ClassUnits.Contains(classunit))
                {
                    classunit.Participants.Remove(user);
                }
                else
                {
                    classunit.Participants.Add(user);
                }
            //}
        }

        private IdentityRole GetRoleById(string roleId)
        {
            return Ctx.Roles.SingleOrDefault(r => r.Id == roleId);
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

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return Ctx.Roles.ToList();
        }
        #endregion

        private ApplicationUser UserWithRoles(ApplicationUser user)
        {
            return Ctx.Users.Include("Roles").Single(u => u.Id == user.Id);
        }

        private bool HasRole(ApplicationUser me, string rolename)
        {
            List<IdentityRole> roles = GetRolesFor(me).ToList();
            return roles.Any(r => r.Name == rolename);
        }
        
        //public ApplicationDbContext Ctx
        //{
        //    get { return Ctx as ApplicationDbContext; }
        //}
    }
}
