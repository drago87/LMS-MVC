using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using Queries.Persistence.Repositories;

namespace Queries.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<Subject>   Subjects   { get; private set; }
        public IRepository<ClassUnit> Classunits { get; private set; }
        public IRepository<Lesson>    Lessons    { get; private set; }
        public IRepository<Folder>    Folders    { get; private set; }
        public IRepository<Dossier>   Dossiers   { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context   = context;
            Subjects   = new Repository<Subject>(_context);
            Classunits = new Repository<ClassUnit>(_context);
            Lessons    = new Repository<Lesson>(_context);
            Folders    = new Repository<Folder>(_context);
            Dossiers   = new Repository<Dossier>(_context);
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