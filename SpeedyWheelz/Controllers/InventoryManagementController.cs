using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpeedyWheelz.Controllers
{
    public class InventoryManagementController : Controller
    {
        private ApplicationDbContext _db;

        public InventoryManagementController() : this(new ApplicationDbContext())
        {
        }

        public InventoryManagementController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: InventoryManagement
        public ActionResult Index(DateTime startDate, DateTime endDate)
        {
            var inventoryData = _db.Inventory
            .Where(i => i.Date >= startDate && i.Date <= endDate)
            .GroupBy(i => i.Product)
            .Select(g => new
            {
                Product = g.Key,
                OriginalQuant = g.Sum(i => i.OriginalQuant),
                QuantSold = g.Sum(i => i.QuantSold),
                RemainingQuant = g.Sum(i => i.RemainingQuant),
                TotalSold = g.Sum(i => i.TotalSold),
                Date = g.Select(i => i.Date).FirstOrDefault()
            })
            .ToList()
            .Select(i => new InventoryManagement
            {
                Product = i.Product,
                OriginalQuant = i.OriginalQuant,
                QuantSold = i.QuantSold,
                RemainingQuant = i.RemainingQuant,
                TotalSold = i.TotalSold,
                Date = i.Date
            })
            .ToList();

            decimal totalSold = inventoryData.Sum(i => i.TotalSold);
            ViewBag.TotalSold = totalSold;

            return View(inventoryData);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}