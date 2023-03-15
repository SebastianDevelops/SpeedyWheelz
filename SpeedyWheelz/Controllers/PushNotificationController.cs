using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Messaging;
using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebPush;

namespace SpeedyWheelz.Controllers
{
    public class PushNotificationController : Controller
    {
        private ApplicationDbContext _db;

        public PushNotificationController() : this(new ApplicationDbContext())
        {
        }

        public PushNotificationController(ApplicationDbContext db)
        {
            _db = db;
        }
            [HttpPost]
            public ActionResult StoreSubscription(string client, string endpoint, string p256dh, string auth)
            {
                pushSubscription subscription = new pushSubscription();
                if (auth == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                // Check if the subscription already exists in the database
                var existingSubscription = _db.pushSubscriptions
                    .FirstOrDefault(s => s.Endpoint == endpoint);

                if (existingSubscription != null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }

                if(!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(p256dh) && !string.IsNullOrEmpty(auth))
                {
                    subscription.ApplicationUserId = User.Identity.GetUserId();
                    subscription.Endpoint = endpoint;
                    subscription.Auth = auth;
                    subscription.P256dh = p256dh;

                    // Store the subscription in the database
                    _db.pushSubscriptions.Add(subscription);
                    _db.SaveChanges();
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                base.Dispose(disposing);
            }

            [HttpPost]
            public ActionResult OrderCreated(string email, string privateKey, string publicKey, string userId, string message)
            { 


            userId = User.Identity.GetUserId();

            var permUsers =   _db.Users
                            .Where(u => u.Roles.Any(r => r.RoleId == "1" || r.RoleId == "2"))
                            .Select(u => u.Id);

            message = "A new order has been placed by " + _db.Users.Where(n => n.Id == userId).Select(n => n.FirstName);

            var vapidDetails = new VapidDetails(email, publicKey, privateKey);

            var webPushClient = new WebPushClient();

            List<PushSubscription> model = new List<PushSubscription>();

            var orderToUsers = _db.pushSubscriptions.ToList();

            foreach (var user in permUsers)
            {
                var subscriptions = (PushSubscription)orderToUsers.Where(u => u.ApplicationUserId == user);

                if(subscriptions != null)
                {
                    model.Add(subscriptions);
                }
            }

            foreach (var order in model)
            {
                webPushClient.SendNotification(order, message, vapidDetails);
            }
                return null;
            }

    }
}