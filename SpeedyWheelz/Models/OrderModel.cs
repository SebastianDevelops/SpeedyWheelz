using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpeedyWheelz.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
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
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CartItemsJsonItems { get; set; }
        public decimal TotalPrice { get; set; }
    }

}