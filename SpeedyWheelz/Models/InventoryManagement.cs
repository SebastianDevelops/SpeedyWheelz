using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Models
{
    public class InventoryManagement
    {
        public InventoryManagement() 
        {
            OriginalQuant = QuantSold + RemainingQuant;
        }

        [Key]
        public int Id { get; set; }
        public string Product { get; set; }
        public int OriginalQuant { get; set; }
        public int QuantSold { get; set; }
        public int RemainingQuant { get; set; }
        public decimal TotalSold { get; set; }
        public DateTime Date { get; set; }
    }
}