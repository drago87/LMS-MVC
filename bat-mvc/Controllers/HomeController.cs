using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext _ctx = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
                return RedirectToAction("TeacherIndex");
            else
                return RedirectToAction("StudentIndex");
        }

        [Authorize(Roles="Teacher")]
       // [ValidateAntiForgeryToken]
        public ActionResult TeacherIndex()
        {
            int id = 3;
            //ClassUnit classunit = _ctx.Classunits.Where(c => c.ClassUnitID == id).Include(x => x.Folders).First();
            return View();
        }

        public ActionResult StudentIndex()
        {
            
            return View();
        }


        public ActionResult App()
        {
            ViewBag.isAuthenticated = (User.Identity.IsAuthenticated) ? "ja" : "nej";
            return View();
        }
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}