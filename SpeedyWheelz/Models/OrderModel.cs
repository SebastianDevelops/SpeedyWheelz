using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [RegularExpression(@"^\+27[0-9]{9}$", ErrorMessage = "Please enter a valid Number(eg: +27 69 0010 0110)")]
        public string PhoneNumber { get; set; }
        public string City { get; set; } = "Johannesburg";
        public string ZipCode { get; set; } = "1830";
        public string Country { get; set; } = "South Africa";

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }


    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string OrderStatus { get; set; }
        public string DriverId { get; set; }
        public bool isAssigned { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CartItemsJsonItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class AdminOrder
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string OrderStatus { get; set; }
        public string DriverId { get; set; }
        public bool isAssigned { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CartItemsJsonItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

}