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
        public ActionResult Index()
        {
            var model = _db.Products.ToList();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name");
            ViewBag.AlcoholCategoryId = new SelectList(_db.AlcoholCategories, "Id", "Name");
            ViewBag.TobaccoCategoryId = new SelectList(_db.TobaccoCategories, "Id", "Name");
            ViewBag.TagId = new SelectList(_db.Tags, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,Price,ImageUrl,CategoryId,AlcoholCategoryId,TobaccoCategoryId,TagId,Description,isAlcohol,isTobacco")] Product product, HttpPostedFileBase image)
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
                        string filePath = Path.Combine(Server.MapPath("~Content/Images"), fileName);

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

        public ActionResult Edit(int? id)
        {
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
        public ActionResult Edit([Bind(Include = "ProductId,Name,Price,ImageUrl,CategoryId,AlcoholCategoryId,TobaccoCategoryId,TagId,Description,isAlcohol,isTobacco")] Product product, HttpPostedFileBase image)
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

        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}