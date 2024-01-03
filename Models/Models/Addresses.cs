using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
namespace ShopShop.Models.Models
{
    public class Addresses
    {
        public static List<Address> GetAll()
        {
            var UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            List<Address> addresses;
            using (var context = new ShoppingCartEntities())
            {
                addresses = ((from addr in context.Addresses where (addr.UserId == UserId) select addr).ToList());
            }
            return addresses;
        }
    }
}