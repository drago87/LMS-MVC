using Awesome.Data;
using Awesome.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace Awesome.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class HomeController : Controller
    {
        AwesomeDbContext db = new AwesomeDbContext();
        //SharedRepository _repo = new SharedRepository();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult About()
        {
            //var userStore = new UserStore<ApplicationUser>(db);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var user = userManager.FindById(User.Identity.GetUserId());

            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            //IdentityRole temp = db.Roles.Single(r => r.Name == "Teacher");

            //if (User.IsInRole("Teacher"))
            //{
            //    throw new NotImplementedException();
            //}

            ViewBag.Message = "Your application description page.";

            return View();
        }

        //public ActionResult ShowAllUsers()
        //{
        //    ViewBag.Message = "A Testpage for now";

        //    return View(_repo.GetAllUsers());
        //}

        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = _repo.GetUserById(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.ClssUnit = _repo.GetAllClasses();
        //    ViewBag.Roles = _repo.GetAllRoles();
        //    return View(applicationUser);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Exclude = "ClassUnitID,RolesId")] ApplicationUser applicationUser, int ClassUnitID, string RolesId)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        _repo.edit(applicationUser, RolesId, ClassUnitID);
        //        return RedirectToAction("ShowAllUsers");
        //    }
        //    //not done
        //    return View(applicationUser);
        //}

        //public ActionResult Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = _repo.GetUserById(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    ViewBag.ClassUnits = _repo.GetUserClassUnitsNameAsList(applicationUser);
        //    ViewBag.Roles = _repo.GetUserRolesNameAsList(applicationUser);

        //    return View(applicationUser);
        //}

        public ActionResult App()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Values()
        {
            return View();
        }
    }
}
