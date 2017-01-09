using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Core.Repositories
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        { }

        public IEnumerable<ApplicationUser> GetUsersByName(string username)
        {
            return Ctx.Users
                .Where(u => u.UserName == username);
        }

        public ApplicationDbContext Ctx
        {
            get { return Ctx as ApplicationDbContext; }
        }
    }
}
