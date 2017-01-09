using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace bat_mvc.Controllers
{
    public class UsersController : Controller
    {
        public readonly IUserRepository _user;
        public readonly IUnitOfWork _uow;

        public UsersController(IUserRepository userRepository, IUnitOfWork uow)
        {
            _user = userRepository;
            _uow = uow;
        }

        public ActionResult Index()
        {
            //var context = new ApplicationDbContext();
            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);

            //var user = userManager.FindById(User.Identity.GetUserId());
            //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            //IdentityRole temp = db.Roles.Single(r => r.Name == "Teacher");

            //if (User.IsInRole("Teacher"))
            //{
            //    throw new NotImplementedException();
            //}
            var users = _user.GetAll();

            return View(users);
        }

        public ActionResult Roles()
        {
            var context     = new ApplicationDbContext();
            var userStore   = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            ApplicationUser user = userManager.FindById(User.Identity.GetUserId());

            //IdentityRole a = Ctx.Roles;
            var roles = _user.GetUserRolesNameAsList(user);
            return View(roles);
        }

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
    }
}
