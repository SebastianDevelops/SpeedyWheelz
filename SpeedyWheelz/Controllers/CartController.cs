using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    [Authorize]
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
        [Authorize]
        public ActionResult Index()
        {
            Cart cart = GetCart();
            return View(cart);
        }
        [Authorize]
        public ActionResult CartItems()
        {
            Cart cart = GetCart();

            return PartialView("_CartItems", cart);
        }
        [Authorize]
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
        [Authorize]
        [HttpPost]
        public ActionResult DecreaseCartItem(int productId, int quantity)
        {
            try
            {
                // Get the cart from the session
                Cart cart = (Cart)Session["cart"];

                // Get the cart item to be decreased
                CartItem item = cart.Items.Where(x => x.Product.ProductId == productId).FirstOrDefault();
                if (item != null)
                {
                    // Decrease the quantity of the item
                    item.Quantity -= quantity;

                    // If the quantity is less than or equal to zero, remove the item from the cart
                    if (item.Quantity <= 0)
                    {
                        cart.RemoveItem(item.Product);
                    }
                }
                else
                {
                    throw new Exception("Invalid product id.");
                }
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception and redirect to an error page
                // You can also show an error message to the user
                var message = ex.InnerException;
                return Json(new { success = false });
            }
        }

        public ActionResult CartCount()
        {
            var cart = GetCart();

            return PartialView("_CartCount", cart);
        }

        [Authorize]
        public ActionResult RemoveFromCart(int productId)
        {
            Product product = GetProduct(productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveItem(product);
                return Json(new { success = true });
            }
            return Json(new { success = false });
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
        [Authorize]
        private Product GetProduct(int productId)
        {
            // Get product from database or other source
            var product = _db.Products.Where(x => x.ProductId == productId).FirstOrDefault();

            return product;
        }
    }
}