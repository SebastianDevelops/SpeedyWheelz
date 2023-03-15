using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductsController() : this(new ApplicationDbContext())
        {
        }

        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Products
        public ActionResult Index(string productType, int? alcoholCategory)
        {
            var products = _db.Products
                            .Where(p => p.isAlcohol == true && p.AlcoholCategoryId == alcoholCategory)
                            .ToList();
            return View(products);
        }

        public ActionResult Details(int id) {
            var product = _db.Products.SingleOrDefault(p => p.ProductId == id);
            if (product == null)
                return HttpNotFound();

            return View(product);
        }

        public ActionResult AlcoholList(string searchText, string searchCategory, int searchCategoryId)
        {
            List<Product> model = new List<Product>();

            if(String.IsNullOrEmpty(searchText))
            {

                model = _db.Products
                            .Where(p => p.isAlcohol == true && p.AlcoholCategoryId == searchCategoryId)
                            .ToList();
            }
            else
            {
                searchText = searchText.ToLower();
                model = _db.Products.Where(p => p.Name.ToLower().Contains(searchText) && p.AlcoholCategory.Name == searchCategory)
               .ToList();
            }
           

            return PartialView("_AlcoholList", model);
        }
    }
}