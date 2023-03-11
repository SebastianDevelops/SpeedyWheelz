using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SpeedyWheelz.Migrations;
using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

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
            var cart = HttpContext.Session["cart"] as Cart;
            if(cart.Items.Count > 0)
            {
                if (cart.TotalPrice > 500)
                {
                    ViewBag.TotalPrice = cart.TotalPrice;
                }
                else
                {
                    ViewBag.TotalPrice = cart.TotalPrice + 30;
                }
            }

            List<string> itemList = new List<string>();

            if(cart.Items.Count > 0)
            {
                foreach (var item in cart.Items)
                {
                    itemList.Add(item.Product.Name);
                }
            }

            ViewBag.itemNames = itemList;

            if (cart.Items.Count > 0)
            {
                ViewBag.CartId = cart.Items.First().Product.Name;
            }

            ViewBag.Quantity = cart.Items.Count;

            ViewBag.Name = User;
            var userId = User.Identity.GetUserId();
            var address = _db.Addresses.Where(m => m.ApplicationUserId == userId).FirstOrDefault();
            return View(address);
        }

        public ActionResult showMore()
        {
            var moreProducts = _db.Products.Take(3).ToList();

            return PartialView("showMore", moreProducts);
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

            return RedirectToAction("Index", "Home");
        }

        ActionResult generateApiSignatureActionResult(Dictionary<string, string> dataArray, string passPhrase = "")
        {
            string payload = "";
            if (passPhrase != "")
            {
                dataArray["passphrase"] = passPhrase;
            }
            Dictionary<string, string> sortedData = dataArray.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            foreach (KeyValuePair<string, string> kvp in sortedData)
            {
                // Get all the data from PayFast and prepare parameter string
                payload += kvp.Key + "=" +
                    Uri.EscapeDataString(kvp.Value.Replace("+", " ")) + "&";
            }
            // After looping through, cut the last & or append your passphrase
            payload = payload.Substring(0, payload.Length - 1);
            string signature = System.Security.Cryptography.
                MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(payload)).
                ToString().ToLowerInvariant();

            return new ContentResult { Content = signature };
        }

        public ActionResult PayFastPayment()
        {
            Guid paymentId= new Guid();
            Dictionary<string, string> pfData = new Dictionary<string, string>
    {
        { "merchant_id", "10028429" },
        { "merchant_key", "wkxzok5x4xvsl" },
        { "return_url", "https://www.example.com" },
        { "notify_url", "https://www.example.com/notify_url" },
        { "m_payment_id", paymentId.ToString() },
        { "amount", "200" },
        { "item_name", "test product" }
    };
            string passPhrase = "jt7NOE43FZPn";
            return generateApiSignatureActionResult(pfData, passPhrase);
        }
    }
}