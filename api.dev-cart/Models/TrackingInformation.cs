﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.dev_cart.Models
{
    public class TrackingInformation
    {
        public int Order_Id { get; set; }
        public string ClientEmail { get; set; }
        public string TrackingId { get; set; }
        public string ShippingService { get; set; }
    }
}