﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.dev_cart.Models
{
    public class CheckoutOrder
    {
        public int Cart_Id { get; set; }
        public string Order_Client_Name { get; set; }
        public string Order_Client_Email { get; set; }
        public string Order_Client_Phone { get; set; }
        public string Address_Street { get; set; }
        public string Address_Number1 { get; set; }
        public string Address_Number2 { get; set; }
        public string Order_Client_Suburb { get; set; }
        public string Order_Client_Province { get; set; }
        public string Order_Client_City { get; set; }
        public string Order_Client_Zip { get; set; }
        public string Order_Client_Comments { get; set; }
        public string Cart_Json { get; set; }
    }
}