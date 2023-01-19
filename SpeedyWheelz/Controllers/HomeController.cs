using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    [Authorize]
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
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var model = _db.Products.ToList().Take(4);

            return View(model);
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