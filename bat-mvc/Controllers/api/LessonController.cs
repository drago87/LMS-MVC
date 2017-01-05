using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Queries.Core;
using Queries.Core.Repositories;
using Queries.Core.Domain;

namespace bat_mvc.Controllers.api
{
    public class LessonController : ApiController
    {
        public readonly IRepository<Lesson> _lessonRepo;
        public readonly IUnitOfWork _uow;

        public LessonController(IRepository<Lesson> lessonRepository, IUnitOfWork uow)
        {
            _lessonRepo = lessonRepository;
            _uow = uow;
        }

        // GET api/Lessons
        [Authorize]
        public IEnumerable<Lesson> Get()
        {
            var lessons = _uow.Lessons.GetAll().OrderByDescending(s => s.StartTime);
            return lessons;
        }

        // GET api/Lessons/5
        public Lesson Get(int id)
        {
            var Lesson = _uow.Lessons.Get(id);
            if (Lesson != null) return Lesson;
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
        }

        //// PUT /api/Lessons/ - Update an existing Lesson
        //public HttpResponseMessage Put([FromBody] Lesson Lesson)
        //{
        //    _uow.Lessons.Update(Lesson);
        //    _uow.Commit();
        //    return new HttpResponseMessage(HttpStatusCode.NoContent);
        //}

        // POST /api/Lessons - Create a new Lesson
        public HttpResponseMessage Post(Lesson Lesson)
        {
            _uow.Lessons.Add(Lesson);
            _uow.Complete(); //Uow.Commit();

            var response = Request.CreateResponse(HttpStatusCode.Created, Lesson);
            return response;
        }

        // DELETE api/movies/5
        public HttpResponseMessage Delete(int id)
        {
            var lesson = _uow.Lessons.Get(id);
            _uow.Lessons.Remove(lesson);
            _uow.Complete();
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
