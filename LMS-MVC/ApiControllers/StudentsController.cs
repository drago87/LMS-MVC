using LMS_MVC.Models;
using LMS_MVC.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace LMS.Controllers
{
    public class StudentsController : ApiController
    {
       private readonly SharedRepository _repo;

        public StudentsController ()
        {
            _repo = new SharedRepository();
        }

        // GET: api/Students
        //[Authorize]
        //public ICollection<ClassUnit> GetStudents()
        //{
        //    //var Students = _repo.GetStudents();
        //    //return Students;
        //    return Ok();
        //}

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClassUnit(int id, ClassUnit classunit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != classunit.ClassUnitID)
            {
                return BadRequest();
            }

            _repo.UpdateClassUnit(classunit);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Students
        [ResponseType(typeof(ClassUnit))]
        public IHttpActionResult PostClassUnit(ClassUnit ClassUnit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Id = _repo.AddClassUnit(ClassUnit);

            return CreatedAtRoute("DefaultApi", new { id = Id }, ClassUnit);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(ClassUnit))]
        public IHttpActionResult DeleteClassUnit(int id)
        {

            var classunit = _repo.RemoveClassUnit(id);

            return Ok(classunit);
        }




    }
}
