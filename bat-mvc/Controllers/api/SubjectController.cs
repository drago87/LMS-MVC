using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bat_mvc.Controllers.api
{
    //[Authorize]
    public class SubjectController : ApiController
    {
        public readonly IRepository<Subject> _subjectRepo;
        public readonly IUnitOfWork _uow;

        public SubjectController(IRepository<Subject> subjectRepository, IUnitOfWork uow)
        {
            _subjectRepo = subjectRepository;
            _uow = uow;
        }

        // GET api/<controller>
        //public IEnumerable<Subject> Get()
        //public String Get()
        public IEnumerable<Subject> Get()
        {
            //return "tjo";
            var subjects = _uow.Subjects.GetAll();
            return subjects;
        }

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}