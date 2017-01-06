using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Awesome.Data.Contracts;
using Awesome.Model;

namespace Awesome.Web.Controllers.Api
{
    public class SubjectsController : ApiBaseController
    {
        public SubjectsController(IAwesomeUow uow)
        {
            Uow = uow;
        }

        // GET api/Subjects
        [Authorize]
        public IQueryable Get()
        {
            var model = Uow.Subjects.GetAll().OrderByDescending(s => s.SubjectName);
            return model;
        }

        // GET api/Subjects/5
        public Subject Get(int id)
        {
            var Subject = Uow.Subjects.GetById(id);
            if (Subject != null) return Subject;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // PUT /api/Subjects/ - Update an existing subject
        public HttpResponseMessage Put([FromBody] Subject Subject)
        {
            Uow.Subjects.Update(Subject);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // POST /api/Subjects - Create a new Subject
        public HttpResponseMessage Post(Subject Subject)
        {
            Uow.Subjects.Add(Subject);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, Subject);
            return response;
        }

        // DELETE api/movies/5
        public HttpResponseMessage Delete(int id)
        {
            Uow.Subjects.Delete(id);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
