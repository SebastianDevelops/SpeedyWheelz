using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Compilation;

namespace SpeedyWheelz.Models
{
    public class DriverModel
    {
        [Key]
        public int DriverId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int OrderId { get; set; }
        public virtual Order orders { get; set; }
    }
}