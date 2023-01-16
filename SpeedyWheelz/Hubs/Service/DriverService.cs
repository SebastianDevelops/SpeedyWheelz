using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Hubs.Service
{
    public class DriverService
    {
        private readonly ApplicationDbContext _db;

        public DriverService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Order GetOrder(int orderId)
        {
            return _db.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
        }
    }
}