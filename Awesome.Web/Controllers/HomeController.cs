using Awesome.Data;
using Awesome.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
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
