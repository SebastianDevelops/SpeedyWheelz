using Newtonsoft.Json;
using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController() : this(new ApplicationDbContext())
        {
        }

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            Session["isAdmin"] = true;
            var model = _db.Products.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            Session["isAdmin"] = true;
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.AlcoholCategoryId = new SelectList(_db.AlcoholCategories, "Id", "Name");
            ViewBag.TobaccoCategoryId = new SelectList(_db.TobaccoCategories, "Id", "Name");
            ViewBag.TagId = new SelectList(_db.Tags, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,ImageUrl,CategoryId,AlcoholCategoryId,TobaccoCategoryId,TagId,Description,isAlcohol,isTobacco,stockCount")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                // Check if a file was uploaded
                if (image != null)
                {
                    // Verify that the file is an image
                    if (image.ContentType.StartsWith("image"))
                    {
                        // Generate a unique file name to prevent overrides
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                        // Generate the file path
                        string filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);

                        // Save the file to the server
                        image.SaveAs(filePath);

                        // Set the ImageUrl property of the product to the file name
                        product.ImageUrl = fileName;
                    }
                }
                
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.AlcoholCategoryId = new SelectList(_db.AlcoholCategories, "Id", "Name", product.AlcoholCategoryId);
            ViewBag.TobaccoCategoryId = new SelectList(_db.TobaccoCategories, "Id", "Name", product.TobaccoCategoryId);
            ViewBag.TagId = new SelectList(_db.Tags, "Id", "Name", product.TagId);
            return View(product);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            Session["isAdmin"] = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.AlcoholCategoryId = new SelectList(_db.AlcoholCategories, "Id", "Name", product.AlcoholCategoryId);
            ViewBag.TobaccoCategoryId = new SelectList(_db.TobaccoCategories, "Id", "Name", product.TobaccoCategoryId);
            ViewBag.TagId = new SelectList(_db.Tags, "Id", "Name", product.TagId);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,ImageUrl,CategoryId,AlcoholCategoryId,TobaccoCategoryId,TagId,Description,isAlcohol,isTobacco,stockCount")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string previousImageName = _db.Products.Where(p => p.ProductId == product.ProductId).Select(p => p.ImageUrl).FirstOrDefault();
                // Check if a file was uploaded
                if (image != null)
                {
                    // Verify that the file is an image
                    if (image.ContentType.StartsWith("image"))
                    {
                        // Generate a unique file name to prevent overrides
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                        // Generate the file path
                        string filePath = Path.Combine(Server.MapPath("~/Content/Images"), fileName);

                        // Save the file to the server
                        image.SaveAs(filePath);

                        // Set the ImageUrl property of the product to the file name
                        product.ImageUrl = fileName;
                    }
                }
                else
                {
                    product.ImageUrl = previousImageName;
                }
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.AlcoholCategoryId = new SelectList(_db.AlcoholCategories, "Id", "Name", product.AlcoholCategoryId);
            ViewBag.TobaccoCategoryId = new SelectList(_db.TobaccoCategories, "Id", "Name", product.TobaccoCategoryId);
            ViewBag.TagId = new SelectList(_db.Tags, "Id", "Name", product.TagId);
            return View(product);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            Session["isAdmin"] = true;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _db.Products.Find(id);
            var imagePath = Path.Combine(Server.MapPath("~/Content/Images"), product.ImageUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult UserOrders()
        {
           var orders = _db.Orders.Include(o => o.Address).Include(o => o.ApplicationUser).ToList();
            return View(orders); 
        }

        [Authorize]
        public ActionResult Orders(string id)
        {
            var orders = _db.Orders.Include(a => a.Address)
                                    .Include(u => u.ApplicationUser)
                                    .Where(o => o.UserId == id).ToList();
            ViewBag.Address = orders[0].Address.Street.ToString();
            ViewBag.CreatedAt = orders[0].CreatedAt.ToString();
            ViewBag.Phone = orders[0].Address.PhoneNumber.ToString();
            return View(orders);
        }
        [Authorize]
        public ActionResult InventoryMangement()
        {
            var orderedProducts = _db.adminOrders.Include(u => u.ApplicationUser).Include(a => a.Address).ToList();

            List<AdminOrder> items = new List<AdminOrder>();

            foreach (var item in orderedProducts)
            {
                var products = JsonConvert.DeserializeObject<List<SpeedyWheelz.Models.CartItem>>(item.CartItemsJsonItems);
            }

            return View();
        }

        [Authorize]
        public ActionResult Inventory(string date)
        {
            return PartialView();
        }

        [Authorize]
        public ActionResult DriverReport()
        {
            return View();
        }

        [Authorize]
        public ActionResult Drivers(DateTime startDate, DateTime endDate)
        {
            var driverIds = _db.Roles.Single(r => r.Name == "Driver").Users.Select(u => u.UserId).ToList();

            var drivers = _db.adminOrders
                        .Where(u => driverIds.Contains(u.DriverId) && u.CreatedAt >= startDate && u.CreatedAt <= endDate)
                        .GroupBy(u => u.DriverId)
                        .Select(g => new DriverViewModel
                        {
                            Driver = _db.Users.Where(u => driverIds.Contains(u.Id) && u.Id == g.Key).FirstOrDefault(),
                            OrderCount = g.Count()
                        })
                        .Where(d => d.Driver != null)
                        .ToList();

            return View(drivers);
        }



    }
}