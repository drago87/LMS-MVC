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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Queries.Core.ViewModels;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext database = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
                return RedirectToAction("TeacherIndex");
            else
                return RedirectToAction("StudentIndex");
        }
        [HttpPost]
        public ActionResult TeacherIndex(string upload)
        {
            return Content("yes");
        }

        [Authorize(Roles="Teacher")]
       // [ValidateAntiForgeryToken]
        public ActionResult TeacherIndex()
        {
            //int id = 3;
            //ClassUnit classunit = database.Classunits.Where(c => c.ClassUnitID == id).Include(x => x.Folders).First();
            


            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


            //List<ClassUnit> classunits = database.Users.SingleOrDefault(x => x.Id == user.Id).ClassUnits.ToList();


            //ViewBag.classes = classunits;

            return View(new MyViewModel() { ClassUnits = database.Classunits.ToList() });

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