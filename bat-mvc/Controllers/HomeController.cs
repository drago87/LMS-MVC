using Queries.Core;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
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
        [HttpPost]
        public ActionResult TeacherIndex(string upload)
        {
            return Content("yes");
        }

        [Authorize(Roles="Teacher")]
       // [ValidateAntiForgeryToken]
        public ActionResult TeacherIndex()
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

            List<ClassUnit> temp = database.Classunits.Include(x => x.Participants).ToList();
            List<string> clssunit = new List<string>();
            foreach (var item in temp)
            {
                if (item.Participants.Count > 0)
                {
                    var temp2 = item.Participants.SingleOrDefault(x => x.UserName == user.UserName);
                    if (temp2.UserName == user.UserName)
                    {
                        clssunit.Add(item.ClassName);
                    }
                }

                

            }


            List<Folder> SharedFolders = new List<Folder>();
            List<Folder> SubmissionFolders = new List<Folder>();

            if (this.User.IsInRole("Teacher") && clssunit.Count > 0)
            {


                foreach (var item in clssunit)
                {
                    SharedFolders.Add(database.Folders.SingleOrDefault(x => x.FolderName == item + "Shared"));
                    SubmissionFolders.Add(database.Folders.SingleOrDefault(x => x.FolderName == item + "Submission"));
                }

            }
            else if (this.User.IsInRole("Student") && clssunit.Count > 0)
            {

                foreach (var item in clssunit)
                {
                    SharedFolders.Add(database.Folders.SingleOrDefault(x => x.FolderName == item + "Shared"));
                }
            }


            var tem6p = database.Folders.Include(x => x.Files).ToList();





            List<FileInfo> filesShared = new List<FileInfo>();


            foreach (var folder in SharedFolders)
            {
                foreach (var file in folder.Files)
                {
                    filesShared.Add(new FileInfo(file.FilePath));
                }
            }

            List<FileInfo> filesSubmission = new List<FileInfo>();


            foreach (var folder in SubmissionFolders)
            {
                foreach (var file in folder.Files)
                {
                    filesSubmission.Add(new FileInfo(file.FilePath));
                }
            }

            return View(new MyViewModel() { ClassUnits = database.Classunits.ToList(), Shared = FileInfoToFileViews(filesShared), Submission = FileInfoToFileViews(filesSubmission) });

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