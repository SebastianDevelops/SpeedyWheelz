using Microsoft.AspNet.Identity;
using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        //Parameterless Constructor

        public HomeController() : this(new ApplicationDbContext())
        {
        }

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            if(User.IsInRole("Driver"))
            {
                return RedirectToAction("Index", "Driver");
            }
            var model = _db.Products.ToList().Take(4);

            return View(model);
        }

        public ActionResult Download()
        {
            return View();
        }

        public ActionResult About()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ProductSearch()
        {
            var userId = User.Identity.GetUserId();
            ViewBag.firstName = _db.Users.Where(u => u.Id == userId).Select(u => u.FirstName).FirstOrDefault();

            return PartialView("_ProuctSearch");
        }

        public ActionResult ProductList(string searchText)
        {
            searchText = searchText.ToLower();
            var products = _db.Products.Where(m => m.Name.ToLower().Contains(searchText)).ToList();

            products.Take(5);

            return PartialView("_ProductList", products);
        }

    }
}