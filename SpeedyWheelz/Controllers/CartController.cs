using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CartController() : this(new ApplicationDbContext())
        { 
        }

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            Cart cart = GetCart();
            return View(cart);
        }

        public ActionResult CartItems()
        {
            Cart cart = GetCart();

            return PartialView("_CartItems", cart);
        }

        public ActionResult AddToCart(int productId, int quantity)
        {
            Product product = GetProduct(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, quantity);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        public ActionResult RemoveFromCart(int productId)
        {
            Product product = GetProduct(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveItem(product);
                return RedirectToAction("Index");
            }
            return HttpNotFound();
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private Product GetProduct(int productId)
        {
            // Get product from database or other source
            var product = _db.Products.Where(x => x.ProductId == productId).FirstOrDefault();

            return product;
        }
    }
}