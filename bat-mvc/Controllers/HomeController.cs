using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
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
            return View();
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