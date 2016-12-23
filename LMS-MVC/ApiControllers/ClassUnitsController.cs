using LMS_MVC.Models;
using LMS_MVC.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMS.Controllers
{
    public class ClassUnitsController : ApiController
    {
       private readonly SharedRepository _repo;

        public ClassUnitsController ()
        {
            _repo = new SharedRepository();
        }

        // GET: api/ClassUnits
        [Authorize]
        public ICollection<ClassUnit> GetClassUnits()
        {
            var classunits = _repo.GetClassUnits();
            return classunits;
        }

    }
}
