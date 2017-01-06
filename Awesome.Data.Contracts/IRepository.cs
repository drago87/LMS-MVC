using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awesome.Data.Contracts
{
    public interface IRepository<T> where T : class
    {
        //To query using LINQ
        IQueryable<T> GetAll();

        //Exempel från video:
        //IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        //Returning Movie or Review by id
        T GetById(int id);

        //Adding Movie or Review
        void Add(T entity);

        //Updating Movie or Review
        void Update(T entity);

        //Deleting Moovie or Review
        void Delete(T entity);

        //Deleting Movie or Review by id
        void Delete(int id);
    }
}