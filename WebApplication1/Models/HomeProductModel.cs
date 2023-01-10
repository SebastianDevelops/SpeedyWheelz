using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class HomeProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}