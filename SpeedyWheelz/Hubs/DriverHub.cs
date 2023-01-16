using Microsoft.AspNet.SignalR;
using SpeedyWheelz.Hubs.Service;
using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Hubs
{
    public class DriverHub : Hub
    {
        private readonly ApplicationDbContext _db;

        public DriverHub() : this(new ApplicationDbContext())
        {
        }

        public DriverHub(ApplicationDbContext db)
        {
            _db = db;
        }

        public void CheckOrderStatus(int orderId)
        {
            var order = _db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
            var status = order.isAssigned ? "Assigned" : "Not Assigned";
            Clients.All.hello(status);
        }
    }
}