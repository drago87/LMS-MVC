using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Repositories;
using Queries.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bat_mvc.Controllers.api
{
    public class ClassUnitController : ApiController // ApiBaseController
    {
        public readonly IRepository<ClassUnit> _classunitRepo;
        public readonly IUnitOfWork _uow;

        public ClassUnitController (IRepository<ClassUnit> classunitRepository, IUnitOfWork uow)
        {
            _classunitRepo = classunitRepository;
            _uow = uow;
        }

        // GET api/classunits
        //[Authorize]
        [AllowAnonymous]
        public IEnumerable<ClassUnitViewModel> Get()
        {
            var classunits = _uow.Classunits.GetAll().OrderByDescending(c => c.ClassName)
                .Select(c => new ClassUnitViewModel
                {
                    ClassUnitID = c.ClassUnitID,
                    ClassName = c.ClassName
                }).ToList();
            return classunits;
        }

        // GET api/classunits/5
        public ClassUnit Get(int id)
        {
            var classunit = _uow.Classunits.Get(id);
            if (classunit != null) return classunit;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // Update an existing classunit
        // PUT /api/classunits/
        public HttpResponseMessage Put([FromBody] ClassUnit classunit)
        {
            //Uow.Classunits.Update(classunit);
            var cu = _uow.Classunits.Get(classunit.ClassUnitID);
            _uow.Complete();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        //// Create a new classunit
        //// POST /api/classunits
        //public HttpResponseMessage Post(ClassUnit classunit)
        //{
        //    Uow.Classunits.Add(classunit);
        //    Uow.Commit();

        //    var response = Request.CreateResponse(HttpStatusCode.Created, classunit);
        //    return response;
        //}

        //// DELETE api/movies/5
        //public HttpResponseMessage Delete(int id)
        //{
        //    Uow.Classunits.Delete(id);
        //    Uow.Commit();
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}
    }
}
