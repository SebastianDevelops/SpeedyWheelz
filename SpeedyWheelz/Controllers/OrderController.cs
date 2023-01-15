using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{

    public class OrderController : Controller
    {
        private ApplicationDbContext _db;
        
        public OrderController() :this(new ApplicationDbContext())
        { }

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Address address)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                address.ApplicationUserId = userId;
                _db.Address.Add(address);
                _db.SaveChanges();
                return RedirectToAction("Checkout");
            }
            return View(address);
        }

        public ActionResult Checkout()
        {
            Order order = new Order();
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(Order order)
        {
            // Retrieve the cart from the session
            var cart = HttpContext.Session["cart"] as Cart;

            // Convert the cart object to a JSON string
            order.CartItemsJsonItems = JsonConvert.SerializeObject(cart.Items);
            order.TotalPrice = cart.TotalPrice;
            order.CreatedAt = DateTime.Now;
            order.OrderStatus = "Paid";
            order.UserId = User.Identity.GetUserId();

            _db.Orders.Add(order);
            _db.SaveChanges();

            return View(order);
        }
    }
}