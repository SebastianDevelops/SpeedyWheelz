using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SpeedyWheelz.Migrations;
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
            Address model = new Address();
            var userId = User.Identity.GetUserId();
            var FirstName = _db.Users.Where(u => u.Id == userId).Select(u => u.FirstName).FirstOrDefault();
            model.FirstName = FirstName;
            var LastName = _db.Users.Where(u => u.Id == userId).Select(u => u.LastName).FirstOrDefault();
            model.LastName = LastName;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Address address)
        {
            var userId = User.Identity.GetUserId();

            var oldAddress = _db.Addresses.FirstOrDefault(a => a.ApplicationUserId == userId);

            if (ModelState.IsValid)
            {
                if(oldAddress != null)
                {
                    oldAddress.Street = address.Street;
                    oldAddress.PhoneNumber= address.PhoneNumber;

                    _db.SaveChanges();
                    return RedirectToAction("Checkout");
                }
                else
                {
                    address.ApplicationUserId = userId;
                    _db.Addresses.Add(address);
                    _db.SaveChanges();
                    return RedirectToAction("Checkout");
                }  
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
            var userId = User.Identity.GetUserId();
            // Retrieve the cart from the session
            var cart = HttpContext.Session["cart"] as Cart;

            // Convert the cart object to a JSON string
            order.AddressId = _db.Addresses.Where(u => u.ApplicationUserId == userId).Select(m => m.AddressId).FirstOrDefault();
            order.CartItemsJsonItems = JsonConvert.SerializeObject(cart.Items);
            order.TotalPrice = cart.TotalPrice;
            order.CreatedAt = DateTime.Now;
            order.OrderStatus = "Paid";
            order.UserId = userId;

            _db.Orders.Add(order);
            _db.SaveChanges();

            return View(order);
        }
    }
}