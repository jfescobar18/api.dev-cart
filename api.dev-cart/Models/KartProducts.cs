using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.dev_cart.Models
{
    public class CartProducts
    {
        public int Product_Id { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Product_Cart_Id { get; set; }
    }
}