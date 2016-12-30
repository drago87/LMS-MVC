using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Awesome.Data.Contracts;
using Awesome.Model;

namespace Awesome.Web.Controllers.Api
{
    public class LessonsController : ApiBaseController
    {
        public LessonsController(IAwesomeUow uow)
        {
            Uow = uow;
        }

        // GET api/Lessons
        [Authorize]
        public IQueryable Get()
        {
            var model = Uow.Lessons.GetAll().OrderByDescending(s => s.StartTime);
            return model;
        }

        // GET api/Lessons/5
        public Lesson Get(int id)
        {
            var Lesson = Uow.Lessons.GetById(id);
            if (Lesson != null) return Lesson;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        // PUT /api/Lessons/ - Update an existing Lesson
        public HttpResponseMessage Put([FromBody] Lesson Lesson)
        {
            Uow.Lessons.Update(Lesson);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        // POST /api/Lessons - Create a new Lesson
        public HttpResponseMessage Post(Lesson Lesson)
        {
            Uow.Lessons.Add(Lesson);
            Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, Lesson);
            return response;
        }

        // DELETE api/movies/5
        public HttpResponseMessage Delete(int id)
        {
            Uow.Lessons.Delete(id);
            Uow.Commit();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
