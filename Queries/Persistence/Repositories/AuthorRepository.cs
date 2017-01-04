//using Queries.Core.Domain;
//using Queries.Core.Models;
//using Queries.Core.Repositories;
//using System.Data.Entity;
//using System.Linq;

//namespace Queries.Persistence.Repositories
//{
//    public class AuthorRepository : Repository<Author>, IAuthorRepository
//    {
//        public AuthorRepository(ApplicationDbContext context) : base(context)
//        {
//        }

//        public Author GetAuthorWithCourses(int id)
//        {
//            //return PlutoContext.Authors.Include(a => a.Courses).SingleOrDefault(a => a.Id == id);
//            return PlutoContext.Authors.SingleOrDefault(a => a.Id == id);
//        }

//        public PlutoContext PlutoContext
//        {
//            get { return Context as PlutoContext; }
//        }
//    }
//}