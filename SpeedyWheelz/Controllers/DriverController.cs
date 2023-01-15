using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Models
{
    public class DriverController : Controller
    {
        // GET: Driver
        public ActionResult Index()
        {
            return View();
        }
    }
}