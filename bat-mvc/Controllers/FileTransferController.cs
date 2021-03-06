﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Queries.Core.Domain;
using Queries.Core.Models;
using Queries.Core.ViewModels;
using System.Data.Entity;

namespace bat_mvc.Controllers
{
    [Authorize]
    public class FileTransferController : Controller
    {
        ApplicationDbContext database = new ApplicationDbContext();

        public ActionResult UploadDocument(string upload, int? ClassUnitID, string uploadMessage = "Choose file.")
        {
            ViewBag.Message = uploadMessage;
            ViewBag.classUnit = ClassUnitID;
            ViewBag.upload = upload;

            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Upload(string uploadTo, string classunit)
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
                        int uploadTo1 = new int();
                        int classunitID = new int();
                        if (classunit != null && classunit != "")
                        {
                            classunitID = int.Parse(classunit);
                        }
                        else
                        {
                            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                            var tempUser = database.Users.Include(p => p.ClassUnits).SingleOrDefault(x => x.UserName == user.UserName);
                            ClassUnit tempClass = tempUser.ClassUnits.FirstOrDefault();
                            classunitID = tempClass.ClassUnitID;
                        }

                        if (uploadTo == "Submission")
                        {
                            uploadTo1 = 1;
                        }
                        else
                        {
                            uploadTo1 = 0;
                        }

                        var path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/") + fileName;
                        if (classunit != null)
                        {
                            Dossier newFile = new Dossier();
                            newFile.FileName = fileName;
                            newFile.FilePath = path;
                            List<ClassUnit> tempList = database.Classunits.Include(x => x.Participants).Include(x => x.Folders).ToList();
                            ClassUnit temp = database.Classunits.SingleOrDefault(x => x.ClassUnitID == classunitID);

                            List<Folder> folders = temp.Folders;
                            Folder ChangedFolder = new Folder();
                            if (folders.Count > 0)
                            {
                                ChangedFolder = folders[uploadTo1];
                            }

                            newFile.Folder = ChangedFolder;

                            database.Dossiers.Add(newFile);
                        }

                        files[i].SaveAs(path);

                        //Database.Folders.SingleOrDefault(x => x.FolderID == _folder.FolderID)
                        //.Files.Add(new Dossier { FileName = files[i].FileName, FilePath = path });

                        database.SaveChanges();
                        message = "The file was uploaded";

                        //}
                        //else
                        //{
                        //    message = "Filename allredy taken.";
                        //}
                    }
                    else
                        message = "You can not upload an empty file.";
                }
            }
            else
                message = "No file selected!";

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
            //files = directory.GetFiles().ToList();

            foreach (var folder in SharedFolders)
            {
                foreach (var file in folder.Files)
                {
                    files.Add(new FileInfo(file.FilePath));
                }
            }

            foreach (var folder in SubmissionFolders)
            {
                foreach (var file in folder.Files)
                {
                    files.Add(new FileInfo(file.FilePath));
                }
            }

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

            return View(FileInfoToFileViews(files));
        }
    }
}