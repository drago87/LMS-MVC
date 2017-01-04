using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using Queries.Persistence.Repositories;

namespace Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly PlutoContext _context;
        private readonly ApplicationDbContext _context;

        //public IAuthorRepository    Authors { get; private set; }
        public IRepository<Subject> Subjects { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            //Authors  = new AuthorRepository(_context);
            Subjects = new Repository<Subject>(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}