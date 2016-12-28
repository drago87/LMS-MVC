using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LMS_MVC.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS_MVC.Repositorys;
using System.Net;

namespace LMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        SharedRepository _repo = new SharedRepository();
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //var userStore = new UserStore<ApplicationUser>(context);

            //var userManager = new UserManager<ApplicationUser>(userStore);

            //var user = userManager.FindById(User.Identity.GetUserId());


            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


            IdentityRole temp = db.Roles.Single(r => r.Name == "Teacher");

            if(User.IsInRole("Teacher"))
            {
                throw new NotImplementedException();
            }
                


            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ShowAllUsers()
        {
            
            ViewBag.Message = "A Testpage for now";

            return View(_repo.GetAllUsers());
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = _repo.GetUserById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.ClssUnit = _repo.GetAllClasses();
            ViewBag.Roles = _repo.GetAllRoles();
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ClassUnitID,RolesId")] ApplicationUser applicationUser, int ClassUnitID, string RolesId)
        {

            if (ModelState.IsValid)
            {
                _repo.edit(applicationUser, RolesId, ClassUnitID);
                return RedirectToAction("ShowAllUsers");
            }
            //not done
            return View(applicationUser);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = _repo.GetUserById(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }


            ViewBag.ClassUnits = _repo.GetUserClassUnitsNameAsList(applicationUser);
            ViewBag.Roles = _repo.GetUserRolesNameAsList(applicationUser);
            
            return View(applicationUser);
            
        }
    }
}