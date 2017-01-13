using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Queries.Core.Models;
using Queries.Core.ViewModels;
using Queries.Core.Domain;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

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

        [Authorize(Roles="Teacher")]
       // [ValidateAntiForgeryToken]
        public ActionResult TeacherIndex()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


            List<ClassUnit> classunits = database.Users.SingleOrDefault(x => x.Id == user.Id).ClassUnits.ToList();

            ViewBag.classes = classunits;

            return View(new MyViewModel() { ClassUnits = classunits});
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