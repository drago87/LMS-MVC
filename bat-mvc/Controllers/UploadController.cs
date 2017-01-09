using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Queries.Core.Repositories;

namespace bat_mvc.Controllers
{
    public class UploadController : Controller
    {


        [Authorize(Roles = "Teacher, Student")]
        public ActionResult UploadDocument()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/") + fileName;
                    file.SaveAs(path);
                    var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                    //_repo.AddFileToClassUnitShared(user, new Dossier { FileName = file.FileName, FilePath = path });

                }
            }

            return RedirectToAction("UploadDocument");
        }
    }
}