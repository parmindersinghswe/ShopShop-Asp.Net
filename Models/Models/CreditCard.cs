using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopShop.Models.Models
{
    public class ExpiryDate
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public ExpiryDate ExpirationDate { get; set; }
        public string CardCode { get; set; }
        public string CardHolderName { get; set; }
       
    }
}