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
        public UserRepository(ApplicationDbContext context) : base(context) { }

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
                //role = 
            }
            return rolenames;
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
