using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Awesome.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        //public ActionResult App() {
        //    return View();
        //}

        //public ActionResult Login() {
        //    return View();
        //}

        //public ActionResult Register() {
        //    return View();
        //}

        //public ActionResult Values() {
        //    return View();
        //
    }
}