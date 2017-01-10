using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core.Domain;
using Queries.Core.Models;
using System.Collections.Generic;

namespace Queries.Core.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void UpdateUser(ApplicationUser user);
        IEnumerable<ApplicationUser> GetUsersByName(string username);
        List<string> GetUserRolesNameAsList(ApplicationUser user);
        IEnumerable<ClassUnit> GetClassUnitsFor(ApplicationUser user);
        ApplicationUser GetUserById(string id);

        void AddUserToRole(ApplicationUser user, IdentityRole role);
    }
}