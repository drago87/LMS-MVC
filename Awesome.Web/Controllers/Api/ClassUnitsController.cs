using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Awesome.Data.Contracts;
using Awesome.Model;

namespace Awesome.Web.Controllers.Api
{
    public class ClassUnitsController : ApiBaseController
    {
        public ClassUnitsController(IAwesomeUow uow)
        {
            Uow = uow;
        }

        // GET api/classunits
        [Authorize]
        public IQueryable Get()
        {
            var model = Uow.Classunits.GetAll().OrderByDescending(c => c.ClassName)
                .Select(c => new ClassUnitViewModel
                {
                    ClassUnitID = c.ClassUnitID,
                    ClassName = c.ClassName
                });
            return model;
        }

        // GET api/classunits/5
        public ClassUnit Get(int id)
        {
            var classunit = Uow.Classunits.GetById(id);
            if (classunit != null) return classunit;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // Update an existing classunit
        // PUT /api/classunits/
        public HttpResponseMessage Put([FromBody] ClassUnit classunit)
        {
            Uow.Classunits.Update(classunit);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // Create a new classunit
        // POST /api/classunits
        public HttpResponseMessage Post(ClassUnit classunit)
        {
            Uow.Classunits.Add(classunit);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, classunit);
            return response;
        }

        // DELETE api/movies/5
        public HttpResponseMessage Delete(int id)
        {
            Uow.Classunits.Delete(id);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
