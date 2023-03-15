using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using SpeedyWheelz.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SpeedyWheelz.Models
{
    [System.Web.Mvc.Authorize]
    [System.Web.Mvc.Authorize(Roles = "Driver")]
    public class DriverController : Controller
    {
        private ApplicationDbContext _db;

        public DriverController(): this(new ApplicationDbContext()) 
        { }

        public DriverController(ApplicationDbContext db) {
            _db = db;
        }
        // GET: Driver
        public ActionResult Index()
        {
            ViewBag.driverId = User.Identity.GetUserId();
            var orders = _db.Orders.Include(o => o.Address).Include(o => o.ApplicationUser).ToList();
            return View(orders);
        }

        public ActionResult OrderDetails(int id)
        {
            var orders = _db.Orders.Include(a => a.Address)
                                    .Include(u => u.ApplicationUser)
                                    .Where(o => o.OrderId == id).ToList();
            return View(orders);
        }

        public ActionResult DriverAssignedOrders(string driverId)
        {
            var orders = _db.Orders.Include(o => o.Address).Include(o => o.ApplicationUser).Where(m => m.DriverId == driverId).ToList();

            return View(orders);
        }

        [HttpPost]
        public async Task<ActionResult> AssignOrder(int orderId)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            if (order == null)
            {
                return new HttpNotFoundResult();
            }
            else
            {
                order.isAssigned = true;
                order.DriverId = User.Identity.GetUserId();
                _db.SaveChanges();

                // Notify clients of the change
                var context = GlobalHost.ConnectionManager.GetHubContext<DriverHub>();
                context.Clients.All.updateOrderStatus(order);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
        }

        public ActionResult FulfillOrder(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _db.Orders.Include(m => m.ApplicationUser).Where(M => M.OrderId == orderId).FirstOrDefault();
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult FulfillOrderConfirmed(int orderId)
        {
            Order order = _db.Orders.Find(orderId);

            AdminOrder adminOrder = new AdminOrder
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                ApplicationUser = order.ApplicationUser,
                AddressId = order.AddressId,
                Address = order.Address,
                OrderStatus = order.OrderStatus,
                DriverId = order.DriverId,
                isAssigned = order.isAssigned,
                CreatedAt = order.CreatedAt,
                CartItemsJsonItems = order.CartItemsJsonItems,
                TotalPrice = order.TotalPrice
            };



            var products = JsonConvert.DeserializeObject<List<SpeedyWheelz.Models.CartItem>>(order.CartItemsJsonItems);

            List<int> ProductIds = new List<int>();

            foreach (var product in products)
            {
                ProductIds.Add(product.Product.ProductId);
            }

            foreach (var productId in ProductIds)
            {
                var product = _db.Products.FirstOrDefault(p => p.ProductId == productId);
                if(product != null)
                {
                    product.stockCount = product.stockCount - products.Where(i => i.Product.ProductId == productId).Select(c => c.Quantity).FirstOrDefault();
                }
                
            }



            _db.Orders.Remove(order);
            _db.SaveChanges();
            return RedirectToAction("DriverAssignedOrders");
        }


    }
}