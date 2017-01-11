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

namespace bat_mvc.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        ApplicationDbContext Database = new ApplicationDbContext();

        //[Authorize(Roles = "Teacher, Student")]
        public ActionResult UploadDocument(string uploadMessage = "Choose file.")
        {
            return View();
        }



        public ActionResult UploadDocumentTest(string uploadMessage = "Choose file.")
        {
            ViewBag.Message = uploadMessage;
            return View();
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload()
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

            return RedirectToAction("UploadDocumentTest", new { uploadMessage = message });
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
            files = directory.GetFiles().ToList();

            //Folder folder = new Folder();
            //foreach (var file in folder.Files.ToList())
            //{
            //    files.Add(new FileInfo(file.FilePath));
            //}

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