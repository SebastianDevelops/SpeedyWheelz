using SpeedyWheelz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Models
{
    public class DriverViewModel
    {
        public ApplicationUser Driver { get; set; }
        public int OrderCount { get; set; }
    }
}