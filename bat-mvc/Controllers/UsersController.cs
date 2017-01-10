using Microsoft.AspNet.Identity;
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
    [Authorize]
    public class UsersController : Controller
    {
        public readonly IUserRepository _user;
        public readonly IUnitOfWork _uow;

        //SharedRepository _repo = new SharedRepository();
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
        [Authorize(Roles="Teacher")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ApplicationUser applicationUser = _repo.GetUserById(id);
            ApplicationUser applicationUser = _user.GetUserById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            List<ClassUnit> classunitsList = new List<ClassUnit>();
            classunitsList.Add(new ClassUnit { ClassName = "---ClassUnits---", ClassUnitID = -1 });
            //classunitsList.AddRange(_repo.GetAllClasses());

            List<IdentityRole> rolesList = new List<IdentityRole>();
            rolesList.Add(new IdentityRole { Name = "---Roles---", Id = "-1" });
            //rolesList.AddRange(_repo.GetAllRoles());

            ViewBag.ClssUnit = classunitsList;
            ViewBag.Roles = rolesList;
            return View(applicationUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "ClassUnitID,RolesId")] ApplicationUser applicationUser, int ClassUnitID, string RolesId)
        {
            if (ModelState.IsValid)
            {
                _repo.edit(applicationUser, RolesId, ClassUnitID);
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //ApplicationUser applicationUser = _repo.GetUserById(id);
            ApplicationUser applicationUser = _user.GetUserById(id);

            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            //List<string> classunits = _repo.GetUserClassUnitsNameAsList(applicationUser);
            var classunits = _user.GetClassUnitsFor(applicationUser);
            //var cu = classunits.Select(c => c.ClassName).ToList().ToString();
            ViewBag.ClassUnits = classunits.Count().ToString();

            //List<string> roles = _repo.GetUserRolesNameAsList(applicationUser);
            ViewBag.Roles = ""; //roles;

            return View(applicationUser);
        }

        [Authorize(Roles="Teacher")]
        //[AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
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

                    return RedirectToAction("Index", "Users");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
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
    }
}
