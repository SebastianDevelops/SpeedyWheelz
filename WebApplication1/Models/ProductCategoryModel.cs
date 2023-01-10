using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Models
{
    public class ProductCategoryModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}