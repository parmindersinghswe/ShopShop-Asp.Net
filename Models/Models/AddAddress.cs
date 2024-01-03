using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace ShopShop.Models.Models
{
    public class AddAddress:Page
    {
        public static bool Add(Address address)
        {
            address.Id = Guid.NewGuid();
            address.UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            address.IsSelected = false;
            using (var context = new ShoppingCartEntities())
            {
                context.Addresses.Add(address);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception) { return false; }
            }
            return true;
        }
        public bool AddToSession(UserBillingAddress address)
        {
           Guid BUserId=Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            using (var context = new ShoppingCartEntities())
            {
                var OBillingAddress = (from addresses in context.BillingAddresses.ToList() select addresses).ToList().FirstOrDefault();
                if (OBillingAddress != null)
                {
                    OBillingAddress.City = address.City;
                    OBillingAddress.FlatNoBuildingName = address.FlatNoBuildingName;
                    OBillingAddress.LocalityOrStreet = address.LocalityOrStreet;
                    OBillingAddress.PinCode = address.PinCode;
                    OBillingAddress.State = address.State;
                    OBillingAddress.LandMark = address.LandMark;
                    OBillingAddress.FirstName = address.FirstName;
                    OBillingAddress.LastName = address.LastName;
                    OBillingAddress.MobileNumber = address.MobileNumber;
                }
                else
                {
                    OBillingAddress = new UserBillingAddress()
                    {

                        Id = Guid.NewGuid(),
                        UserId = BUserId,
                        City = address.City,
                        FlatNoBuildingName = address.FlatNoBuildingName,
                        LocalityOrStreet = address.LocalityOrStreet,
                        PinCode = address.PinCode,
                        State = address.State,
                        LandMark = address.LandMark,
                        FirstName = address.FirstName,
                        LastName=address.LastName,
                        MobileNumber = address.MobileNumber,
                    };
                    context.BillingAddresses.Add(OBillingAddress);

                }
                    context.SaveChanges();
            }
                return true;
        }
    }
}