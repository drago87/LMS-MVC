using LMS_MVC.Repositorys;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Core.Domain;
using Queries.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;

namespace bat_mvc.Controllers
{
    public class HomeController : Controller
    {

        SharedRepository _repo = new SharedRepository();
        private ApplicationUserManager _userManager;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult App()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticated = "ja";
            }
            else
            {
                ViewBag.IsAuthenticated = "nej";
            }
            return View();
        }

        [Authorize]
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

            List<ClassUnit> classunitsList = new List<ClassUnit>();
            classunitsList.Add(new ClassUnit { ClassName = "---ClassUnits---", ClassUnitID = -1});
            classunitsList.AddRange(_repo.GetAllClasses());

            List<IdentityRole> rolesList = new List<IdentityRole>();
            rolesList.Add(new IdentityRole { Name = "---Roles---", Id = "-1" });
            rolesList.AddRange(_repo.GetAllRoles());

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

        //[Authorize(Roles="Teacher")]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
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

                    return RedirectToAction("Index", "Home");
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
        
    }
}