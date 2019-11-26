﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.dev_cart.Models
{
    public class AddressToSkydropx
    {
        public string province { get; set; }
        public string city { get; set; }
        public string name { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string address1 { get; set; }
        public string company { get; set; }
        public string address2 { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string references { get; set; }
        public string contents { get; set; }
    }
}