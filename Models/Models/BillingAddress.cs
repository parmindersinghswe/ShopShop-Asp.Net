using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopShop.Models.Models
{
    public class BillingAddress
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
    }
}