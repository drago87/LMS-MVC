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
using bat_mvc.ViewModels;

namespace bat_mvc.Controllers
{
    public class UploadController : Controller
    {
        ApplicationDbContext Database = new ApplicationDbContext();

        //[Authorize(Roles = "Teacher, Student")]
        public ActionResult UploadDocument()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload()
        {
            //var folder = Database.Folders.SingleOrDefault(x => x.FolderID == _folder.FolderID);
            //string message = "The file was uploaded";

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    //if (folder.Files.SingleOrDefault(x => x.FileName == fileName) == null)
                    //{


                    var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/") + fileName;
                    file.SaveAs(path);

                    //Database.Folders.SingleOrDefault(x => x.FolderID == _folder.FolderID)
                    //.Files.Add(new Dossier { FileName = file.FileName, FilePath = path });

                    //}
                    //else
                    //    message = "Filename allredy taken.";
                }
            }

            return RedirectToAction("UploadDocument");
        }

        public ActionResult Show()
        {
            DirectoryInfo directory = new DirectoryInfo(Server.MapPath(@"~\Files"));
            var files = directory.GetFiles().ToList();
            List<FileViewModel> fileViews = new List<FileViewModel>();
            foreach (var item in files)
            {
                byte[] byteArr;
                try
                {
                    //Bitmap iconImage = (Bitmap)Image.FromFile(item.FullName);
                    //iconImage.SetResolution(72,72);

                    Image image = Image.FromFile(item.FullName);
                    Image newImage = image.GetThumbnailImage(50, 50, null, new IntPtr());


                    ImageConverter converter = new ImageConverter();
                    byteArr = (byte[])converter.ConvertTo(newImage, typeof(byte[]));
                }
                catch (Exception)
                {
                    Icon icon = Icon.ExtractAssociatedIcon(item.FullName);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        icon.Save(ms);
                        byteArr = ms.ToArray();
                    }


                }

                var base64 = Convert.ToBase64String(byteArr);
                var imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                fileViews.Add(new FileViewModel() { IconImg = imgSrc, File = item });
            }
            //var img = Image.FromFile(System.Web.Hosting.HostingEnvironment.MapPath("~/Files/") + "Jellyfish.jpg");



            //ViewBag.fileViews = fileViews;

            return View(fileViews);
        }



    }
}