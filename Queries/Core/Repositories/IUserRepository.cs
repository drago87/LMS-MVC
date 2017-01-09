using Queries.Core.Domain;
using Queries.Core.Models;
using System.Collections.Generic;

namespace Queries.Core.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        IEnumerable<ApplicationUser> GetUsersByName(string username);
    }
}