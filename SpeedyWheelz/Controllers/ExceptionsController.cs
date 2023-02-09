using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    public class ExceptionsController : Controller
    {
        // GET: Exceptions
        public ActionResult Return()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Notify()
        {
            Address address = new Address();

            var m_payment_id = Request.Form["m_payment_id"];
            var pf_payment_id = Request.Form["pf_payment_id"];
            var payment_status = Request.Form["payment_status"];
            var item_name = Request.Form["item_name"];
            var merchant_id = Request.Form["merchant_id"];
            var token = Request.Form["token"];
            Response.StatusCode = 200;
            if (payment_status == "COMPLETE")
            {
                return null;
            }

            return null;
        }

        public ActionResult Cancel()
        {
            return View();
        }

    }
}