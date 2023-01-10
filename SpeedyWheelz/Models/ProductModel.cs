using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Models
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
        [Required(ErrorMessage = "Product Name Is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Price Is Required")]
        [Display(Name = "Price(R)")]
        public int Price { get; set; }
        [Required(ErrorMessage = "An Image Is Required For Every Product")]
        [Display(Name = "Select an image")]
        public string ImageUrl { get; set; }
        [ForeignKey("Category")]
        [Required(ErrorMessage = "Please select a Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey("AlcoholCategory")]
        [Required(ErrorMessage = "Please select a Category of Alcohol")]
        [Display(Name = "Category Of Alcohol")]
        public int? AlcoholCategoryId { get; set; }
        public virtual AlcoholCategory AlcoholCategory { get; set; }
        [ForeignKey("TobaccoCategory")]
        [Required(ErrorMessage = "Please select a Category of Tobacco")]
        [Display(Name = "Category Of Tobacco")]
        public int TobaccoCategoryId { get; set; }
        public virtual TobaccoCategory TobaccoCategory { get; set; }
        [ForeignKey("Tag")]
        [Required(ErrorMessage = "Please select a Tag")]
        [Display(Name = "Tag")]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        [Display(Name="Is This Alcohol?")]
        public bool isAlcohol { get; set; }
        [Display(Name = "Is This Tobacco?")]
        public bool isTobacco { get; set; }
    }

}