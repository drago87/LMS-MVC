using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace WebApplication1.api
{
    public class FileUploadController : ApiController
    {


        [HttpGet]
        public string hello()
        {
            return "Jello";
        }


        [HttpPost()]
        public string UploadFiles()
        {
            int iUploadCnt = 0;
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/");

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            for (int iCnt = 0; iCnt < hfc.Count; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];
                string fileName = Path.GetFileName(hpf.FileName);

                if (hpf.ContentLength > 0 && !File.Exists(sPath + fileName))
                {
                    hpf.SaveAs(sPath + fileName);
                    iUploadCnt += 1;
                }
            }


            if (iUploadCnt > 0)
            {
                return iUploadCnt + "Files Uploaded Successfully";
            }
            else
            {
                return "Upload failed";
            }



        }

    }
}
