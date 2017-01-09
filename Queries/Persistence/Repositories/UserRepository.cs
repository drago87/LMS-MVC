using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core.Domain;
using Queries.Core.Models;
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
        public UserRepository(ApplicationDbContext context) : base(context) { }

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

        public List<string> GetUserRolesNameAsList(ApplicationUser user)
        {
            //List<string> tempRoleNames = new List<string>();

            //if (user.Roles != null && user.Roles.Count > 0)
            //{
            //    foreach (var role in user.Roles)
            //    {
            //        //tempRoleNames.Add(GetRoleById(role.RoleId).Name);
            //        tempRoleNames.Add(role.RoleId.ToString());
            //    }
            //}
            //else
            //{
            //    tempRoleNames.Add("Not assigned a Role yet!");
            //}

            //return tempRoleNames;
            return user.Roles.Select(r => r.RoleId.ToString()).ToList();
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
        #endregion

        public ApplicationDbContext Ctx
        {
            get { return Ctx as ApplicationDbContext; }
        }
    }
}
