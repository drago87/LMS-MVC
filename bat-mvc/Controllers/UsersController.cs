using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core;
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
        //public readonly IRepository<User> _subjectRepo;
        //public readonly IUnitOfWork _uow;

        //public UsersController (IRepository<User> subjectRepository, IUnitOfWork uow)
        //{
        //    _userRepo = userRepository;
        //    _uow = uow;
        //}

        public ActionResult Index()
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
    }
}
