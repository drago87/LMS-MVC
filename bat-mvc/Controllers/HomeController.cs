using Queries.Core.Domain;
using Queries.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Queries.Core.ViewModels;
using System.Drawing;
using System.IO;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationDbContext database = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (User.IsInRole("Teacher"))
                return RedirectToAction("TeacherIndex");
            else
                return RedirectToAction("StudentIndex");
        }

        [Authorize(Roles = "Teacher")]
        // [ValidateAntiForgeryToken]
        public ActionResult TeacherIndex(int? ClassUnitID)
        {
            Func<List<FileInfo>, List<FileViewModel>> FileInfoToFileViews = (fileInfo) =>
            {
                List<FileViewModel> fileViews = new List<FileViewModel>();
                foreach (var item in fileInfo)
                {
                    byte[] byteArr;
                    try
                    {
                        Image image = Image.FromFile(item.FullName);
                        ImageConverter converter = new ImageConverter();
                        byteArr = (byte[])converter.ConvertTo(image, typeof(byte[]));
                    }
                    catch (Exception)
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(item.FullName);

                        Image image = new Icon(icon, 32, 32).ToBitmap();

                        ImageConverter converter = new ImageConverter();
                        byteArr = (byte[])converter.ConvertTo(image, typeof(byte[]));

                    }

                    var base64 = Convert.ToBase64String(byteArr);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    fileViews.Add(new FileViewModel() { IconImg = imgSrc, File = item });
                }
                return fileViews;
            };

            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~\Files"));
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ClassUnit classUnit = database.Classunits.Include(z => z.Folders).SingleOrDefault(x => x.ClassUnitID == ClassUnitID);
            Folder SharedFolder = new Folder();
            Folder SubmissionFolder = new Folder();

            if (classUnit != null)
            {
                SharedFolder = classUnit.Folders.ToList()[0];
                SubmissionFolder = classUnit.Folders.ToList()[1];
            }

            var tem6p = database.Folders.Include(x => x.Files).ToList();

            List<FileInfo> filesShared = new List<FileInfo>();
            foreach (var file in SharedFolder.Files)
            {
                filesShared.Add(new FileInfo(file.FilePath));
            }

            List<FileInfo> filesSubmission = new List<FileInfo>();
            foreach (var file in SubmissionFolder.Files)
            {
                filesSubmission.Add(new FileInfo(file.FilePath));
            }

            return View(new MyViewModel() { ClassUnits = database.Classunits.ToList(), Shared = FileInfoToFileViews(filesShared), Submission = FileInfoToFileViews(filesSubmission) });
        }

        [Authorize(Roles = "Student")]
        public ActionResult StudentIndex()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var tempUser = database.Users.Include(p => p.ClassUnits).SingleOrDefault(x => x.UserName == user.UserName);
            ClassUnit tempClass = tempUser.ClassUnits.FirstOrDefault();
            int? ClassUnitID = null;
            if (tempClass != null)
            {
                ClassUnitID = tempClass.ClassUnitID;
                ViewBag.Message = "StudentIndex";
            }
            else
                ViewBag.Message = "You are not part of a class yet.";

            Func<List<FileInfo>, List<FileViewModel>> FileInfoToFileViews = (fileInfo) =>
            {
                List<FileViewModel> fileViews = new List<FileViewModel>();
                foreach (var item in fileInfo)
                {
                    byte[] byteArr;
                    try
                    {
                        Image image = Image.FromFile(item.FullName);
                        ImageConverter converter = new ImageConverter();
                        byteArr = (byte[])converter.ConvertTo(image, typeof(byte[]));
                    }
                    catch (Exception)
                    {
                        Icon icon = Icon.ExtractAssociatedIcon(item.FullName);

                        Image image = new Icon(icon, 32, 32).ToBitmap();

                        ImageConverter converter = new ImageConverter();
                        byteArr = (byte[])converter.ConvertTo(image, typeof(byte[]));

                    }

                    var base64 = Convert.ToBase64String(byteArr);
                    var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    fileViews.Add(new FileViewModel() { IconImg = imgSrc, File = item });
                }
                return fileViews;
            };

            ClassUnit classUnit = database.Classunits.Include(z => z.Folders).SingleOrDefault(x => x.ClassUnitID == ClassUnitID);
            Folder SharedFolder = new Folder();
            Folder SubmissionFolder = new Folder();

            if (classUnit != null)
            {
                SharedFolder = classUnit.Folders.ToList()[0];
                SubmissionFolder = classUnit.Folders.ToList()[1];
            }

            var tem6p = database.Folders.Include(x => x.Files).ToList();

            List<FileInfo> filesShared = new List<FileInfo>();
            foreach (var file in SharedFolder.Files)
            {
                filesShared.Add(new FileInfo(file.FilePath));
            }

            List<FileInfo> filesSubmission = new List<FileInfo>();
            foreach (var file in SubmissionFolder.Files)
            {
                filesSubmission.Add(new FileInfo(file.FilePath));
            }

            return View(new MyViewModel() { ClassUnits = database.Classunits.ToList(), Shared = FileInfoToFileViews(filesShared), Submission = FileInfoToFileViews(filesSubmission) });
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