using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopShop.Models.Models
{
    public class ProxyUserDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}