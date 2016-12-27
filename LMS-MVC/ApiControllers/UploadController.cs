using LMS_MVC.Repositorys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using LMS_MVC.Models;
using System.Net.Http;
using System.Web.Http;

namespace LMS_MVC.Controllers
{
    public class UploadController : ApiController
    {
        SharedRepository _repo = new SharedRepository();


        [Authorize(Roles = "Teacher, Student")]
        public IHttpActionResult UploadDocument()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Upload()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    //var path = Path.Combine(Server.MapPath(@"C:\Files"), fileName);
                    var path = @"C:\Files\" + fileName;
                    file.SaveAs(path);
                    var user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                    _repo.AddFileToClassUnitShared(user, new Dossier { FileName = file.FileName, FilePath = path });
                    
                }
            }

            return RedirectToAction("UploadDocument");
        }
    }
}

/*
 * string path1 = @"C:\Users\Public\TestFolder\Unsorted.txt";
 * string path1 = @"../TestFolder\Unsorted.txt";
 * 
 * 
 * if (!File.Exists(path1))
            {
                var myFile = File.Create(path1);
                myFile.Close();
                PrintToFile(path1);  
            }
 * 
 * public static void PrintToFile(string thefile)
        {
          System.IO.File.WriteAllLines(thefile,content);
           
        }
 */
