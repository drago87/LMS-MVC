﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace bat_mvc.Controllers
{
    [Authorize(Roles="Teacher")]
    public class UsersController : Controller
    {
        public readonly IUserRepository _user;
        public readonly IUnitOfWork _uow;

        public ApplicationDbContext _ctx = new ApplicationDbContext();

        private ApplicationUserManager _userManager;

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
            var users = _ctx.Users.ToList();

            return View(users.OrderBy(x => x.Email));
        }

        //public ActionResult Roles()
        //{
        //    var context     = new ApplicationDbContext();
        //    var userStore   = new UserStore<ApplicationUser>(context);
        //    var userManager = new UserManager<ApplicationUser>(userStore);
        //    ApplicationUser user = userManager.FindById(User.Identity.GetUserId());

        //    //IdentityRole a = Ctx.Roles;
        //    var roles = _user.GetUserRolesNameAsList(user);
        //    return View(roles);
        //}

        //[Authorize(Roles = "Teacher")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ApplicationUser applicationUser = _user.GetUserById(id);
            ApplicationUser applicationUser = _ctx.Users.SingleOrDefault(x => x.Id == id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            List<ClassUnit> classunitsList = new List<ClassUnit>();
            classunitsList.Add(new ClassUnit { ClassName = "---ClassUnits---", ClassUnitID = -1 });
            classunitsList.AddRange(_uow.Classunits.GetAll());

            List<IdentityRole> rolesList = new List<IdentityRole>();
            rolesList.Add(new IdentityRole { Name = "---Roles---", Id = "-1" });
            rolesList.AddRange(_user.GetAllRoles());

            ViewBag.ClassUnits = classunitsList;
            ViewBag.Roles = rolesList;
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ClassUnitID,RolesId")] ApplicationUser applicationUser, int ClassUnitID, string RolesId)
        {
            if (ModelState.IsValid)
            {
                _user.edit(applicationUser, RolesId, ClassUnitID);
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }
        [Authorize(Roles = "Teacher")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ApplicationUser applicationUser = _user.GetUserById(id);
            ApplicationUser applicationUser = _ctx.Users.SingleOrDefault(x => x.Id == id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            ViewBag.MyClasses = GetMyClassNamesAsString(applicationUser);
            ViewBag.MyRoles   = GetMyRolesAsString(applicationUser);

            return View(applicationUser);
        }

        [Authorize(Roles = "Teacher")]
        //[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    ViewBag.Wrong = "";
                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.Wrong = "Something when wrong. Did you write the same password twice?";
            return View(model);
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion

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

        private string GetMyRolesAsString(ApplicationUser applicationUser)
        {
            var roles = _user.GetRolesFor(applicationUser);
            var rolenames = roles.Select(r => r.Name).ToArray();
            return String.Join(",", rolenames);
        }

        private string GetMyClassNamesAsString(ApplicationUser applicationUser)
        {
            var classunits = _user.GetClassUnitsFor(applicationUser);
            var classnames = classunits.Select(c => c.ClassName).ToArray();
            return String.Join(",", classnames);
        }
    }
}
