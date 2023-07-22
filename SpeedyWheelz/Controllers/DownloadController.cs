using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DownloadApk(string device)
        {
            string filePath = "";

            if(device == "android")
            {
                // Replace the file path with the path to your APK file
                filePath = Server.MapPath("~/apk/SpeedyWheelz.apk");
            }
            //else if(device == "ios")
            //{
            //    // Replace the file path with the path to your APK file
            //    filePath = Server.MapPath("~/apk/SpeedyWheelz.apk");
            //}

            // Set the content type of the file
            string contentType = MimeMapping.GetMimeMapping(filePath);

            // Return the file as a download
            return File(filePath, contentType, "SpeedyWheelz.apk");
        }

    }
}