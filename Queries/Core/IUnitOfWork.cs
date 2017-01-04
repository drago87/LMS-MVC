using Queries.Core.Domain;
using Queries.Core.Repositories;
using System;

namespace Queries.Core
{
    public interface IUnitOfWork : IDisposable
    {
        //IAuthorRepository Authors { get; }
        IRepository<Subject> Subjects { get; }
        int Complete();
    }
}