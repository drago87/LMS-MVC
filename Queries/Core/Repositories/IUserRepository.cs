using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core.Domain;
using Queries.Core.Models;
using System.Collections.Generic;

namespace Queries.Core.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void edit(ApplicationUser user, string roleId, int classunitId);
        void UpdateUser(ApplicationUser user);
        IEnumerable<ApplicationUser> GetUsersByName(string username);
        IEnumerable<ClassUnit> GetClassUnitsFor(ApplicationUser user);
        IEnumerable<IdentityRole> GetRolesFor(ApplicationUser user);
        IEnumerable<IdentityRole> GetAllRoles();
        ApplicationUser GetUserById(string id);

        void AddUserToRole(ApplicationUser user, IdentityRole role);
    }
}