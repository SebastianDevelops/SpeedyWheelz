using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class AlcoholCategory
    {
        [Key]
        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class TobaccoCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("AlcoholCategory")]
        public int? AlcoholCategoryId { get; set; }
        public virtual AlcoholCategory AlcoholCategory { get; set; }
        [ForeignKey("TobaccoCategory")]
        public int TobaccoCategoryId { get; set; }
        public virtual TobaccoCategory TobaccoCategory { get; set; }
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        public string Description { get; set; }
        public bool isAlcohol { get; set; }
        public bool isTobacco { get; set; }
    }
}