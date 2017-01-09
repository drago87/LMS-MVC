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
