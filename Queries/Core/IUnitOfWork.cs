using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;

namespace Queries.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Subject>         Subjects   { get; }
        IRepository<ClassUnit>       Classunits { get; }
        IRepository<Lesson>          Lessons    { get; }
        IRepository<Folder>          Folders    { get; }
        IRepository<Dossier>         Dossiers   { get; }
        IRepository<ApplicationUser> Users      { get; }
        int Complete();
    }
}