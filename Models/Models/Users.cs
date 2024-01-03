using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ShopShop.Models.Models
{
    public class Users
    {
        public static bool DeleteAddress(Guid id)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.Addresses.Remove((from addr in context.Addresses where addr.Id == id select addr).ToList().FirstOrDefault());
                try
                {
                    context.SaveChanges();
                }
                catch (Exception) { return false; }
            }
            return true;
        }
        public static bool UpdateSelectedAddress(Guid AddressId)
        {
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            using (var context = new ShoppingCartEntities())
            {
                var Unselect = (from UserAddress in context.Addresses where (UserAddress.IsSelected == true && UserAddress.UserId==UserId) select UserAddress).ToList().FirstOrDefault();
                var Select = (from SelectableAddress in context.Addresses where (SelectableAddress.Id == AddressId) select SelectableAddress).ToList().FirstOrDefault();
                if (Unselect != null) Unselect.IsSelected = false;
                Select.IsSelected = true;
                try
                {
                    context.SaveChanges();
                }
                catch (Exception) { return false; }

            }
            return true;
        }
        public static Address SelectedAddress()
        {
            Address selected;
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            using (var context = new ShoppingCartEntities())
            {
                selected = (from address in context.Addresses where (address.IsSelected == true && address.UserId == UserId) select address).ToList().FirstOrDefault();
            }
            return selected;
        }
    }
}