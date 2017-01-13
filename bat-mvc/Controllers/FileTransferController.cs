using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Queries.Core.Repositories;
using Queries.Core.Domain;
using Queries.Core;
using Queries.Core.Models;
using Queries.Core.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Queries.Persistence.Repositories;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;


namespace bat_mvc.Controllers
{
    [Authorize]
    public class FileTransferController : Controller
    {
        ApplicationDbContext Database = new ApplicationDbContext();



        public ActionResult UploadDocumentShared(string uploadMessage = "Choose file.")
        {
            ViewBag.Message = uploadMessage;
            return View();
        }

        public ActionResult UploadDocumentSubmission(string uploadMessage = "Choose file.")
        {
            ViewBag.Message = uploadMessage;
            return View();
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload(int uploadTo)
        {
            //var folder = Database.Folders.SingleOrDefault(x => x.FolderID == _folder.FolderID);
            string message = "The file was not uploaded";
            var files = Request.Files;


            if (files.Count > 0)
            {


                for (int i = 0; i < files.Count; i++)
                {
                    if (files[i] != null && files[i].ContentLength > 0)
                    {





                        var fileName = Path.GetFileName(files[i].FileName);
                        //if (folder.Files.SingleOrDefault(x => x.FileName == fileName) == null)
                        //{


                        var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/") + fileName;
                        files[i].SaveAs(path);

                        //Database.Folders.SingleOrDefault(x => x.FolderID == _folder.FolderID)
                        //.Files.Add(new Dossier { FileName = files[i].FileName, FilePath = path });

                        Database.SaveChanges();
                        message = "The file was uploaded";

                        //}
                        //else
                        //{
                        //    message = "Filename allredy taken.";
                        //}
                    }
                }
            }

            return RedirectToAction("UploadDocument", new { uploadMessage = message });
        }



        public ActionResult SaveDocument(string filePath)
        {
            string contentType = "application/octet-stream";  //<---- This is the magic
            FileInfo file = new FileInfo(filePath);

            return File(filePath, contentType, file.Name);
        }


        public ActionResult Show()
        {
            List<FileInfo> files = new List<FileInfo>();

            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~\Files"));
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            List<ClassUnit> temp = Database.Classunits.Include(x => x.Participants).ToList();
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
                    SharedFolders.Add(Database.Folders.SingleOrDefault(x => x.FolderName == item + "Shared"));
                    SubmissionFolders.Add(Database.Folders.SingleOrDefault(x => x.FolderName == item + "Submission"));
                }

            }
            else if (this.User.IsInRole("Student") && clssunit.Count > 0)
            {

                foreach (var item in clssunit)
                {
                    SharedFolders.Add(Database.Folders.SingleOrDefault(x => x.FolderName == item + "Shared"));
                }
            }


            var tem6p = Database.Folders.Include(x => x.Files).ToList();
            //files = directory.GetFiles().ToList();


            foreach (var folder in SharedFolders)
            {
                foreach (var file in folder.Files)
                {
                    files.Add(new FileInfo(file.FilePath));
                }
            }


            List<FileViewModel> fileViews = new List<FileViewModel>();


            foreach (var item in files)
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

                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    icon.Save(ms);
                    //    byteArr = ms.ToArray();
                    //}
                }

                var base64 = Convert.ToBase64String(byteArr);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                fileViews.Add(new FileViewModel() { IconImg = imgSrc, File = item });
            }

            return View(fileViews);
        }



    }
}