using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpeedyWheelz.Models;

namespace SpeedyWheelz.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }

        public void AddItem(Product product, int quantity)
        {
            CartItem item = Items.Where(x => x.Product.ProductId == product.ProductId).FirstOrDefault();
            if (item == null)
            {
                item = new CartItem()
                {
                    Product = product,
                    Quantity = quantity
                };
                Items.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product)
        {
            CartItem item = Items.Where(x => x.Product.ProductId == product.ProductId).FirstOrDefault();
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal TotalPrice
        {
            get { return Items.Sum(x => x.TotalPrice); }
        }
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice
        {
            get { return Product.Price * Quantity; }
        }
    }

}