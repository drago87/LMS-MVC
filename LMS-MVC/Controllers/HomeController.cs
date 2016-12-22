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

namespace LMS_MVC.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        Repository _repo = new Repository();
        

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

        public ActionResult Edit()
        {
            ViewBag.ClssUnit = _repo.GetAllClasses();
            ViewBag.Roles = _repo.GetAllRoles();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,UserName,Roles")] ApplicationUser applicationUser)
        {
            //not done
            return View();
        }
    }
}