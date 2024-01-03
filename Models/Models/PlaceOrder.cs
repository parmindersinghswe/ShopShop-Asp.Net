using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ShopShop.Models.Models
{
    public class PlaceOrder
    {
        public static bool Order(Guid OrderId, Guid OUserId, DataBaseModels.ProductsOrderList productsorderlist)
        {
            using (var context = new ShoppingCartEntities())
            {
                productsorderlist.Id = Guid.NewGuid();
                productsorderlist.OrderId = OrderId;
                productsorderlist.Price = (from product in context.Products where (product.Id == productsorderlist.ProductId) select product.Price).ToList().FirstOrDefault();
                context.ProductsOrderLists.Add(productsorderlist);
                try

                {
                    context.SaveChanges();
                }
                catch (Exception) { return false; }
            }
            return true;
        }
        public static Guid OrderProduct(ProductsOrderList productsorderlist)
        {
            Guid OrderId = Guid.NewGuid();
            Guid OUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            User User;
            using (var context = new ShoppingCartEntities())
            {
                User = (from user in context.Users where (user.Id == OUserId) select user).ToList().FirstOrDefault();
                context.Orders.Add(
                   new Order()
                   {
                       Id = OrderId,
                       UserId = OUserId,
                       AddressId = GetAddress(OUserId, OrderId),
                       //Address = (from address in context.Addresses where ((address.UserId == OUserId) && (address.IsSelected)) select address.City).ToList().FirstOrDefault(),
                       OrderDate = DateTime.Now,
                       Status = "Just Ordered",
                       ReceiveDate = new DateTime(2018, 9, 25),
                       ModifiedBy = User,
                       CreatedBy = User,//(from user in context.Users where (user.Id == OUserId) select user).ToList().FirstOrDefault(),
                       OrderTotalAmount =productsorderlist.ProductQty*(from product in context.Products where (product.Id == productsorderlist.ProductId) select product.Price).ToList().FirstOrDefault()
                   });
                try

                {
                    context.SaveChanges();
                }
                catch (Exception) { return new Guid(); }
            }
            if (Order(OrderId, OUserId, productsorderlist))
                return OrderId;
            return new Guid();
        }
        public static Guid OrderCartProducts()
        {
            Guid OrderId = Guid.NewGuid();
            Guid OUserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            User User;
            bool success = true;

            decimal TotalPrice = 0;

            using (var context = new ShoppingCartEntities())
            {
                User = (from user in context.Users where (user.Id == OUserId) select user).ToList().FirstOrDefault();
                context.Orders.Add(
                   new Order()
                   {
                       Id = OrderId,
                       UserId = OUserId,
                       AddressId = GetAddress(OUserId, OrderId),
                       // Address = (from address in context.Addresses where ((address.UserId == OUserId) && (address.IsSelected)) select address.City).ToList().FirstOrDefault(),
                       OrderDate = DateTime.Now,
                       Status = "Just Ordered",
                       ReceiveDate = new DateTime(2018, 9, 25),
                       CreatedBy = User,
                       ModifiedBy = User,
                       OrderTotalAmount = TotalPrice
                   });
                try

                {
                    context.SaveChanges();
                }
                catch (Exception) { return new Guid(); }
            }

            using (var context = new ShoppingCartEntities())
            {
                var list = (from cart in context.Carts where (cart.UserId == OUserId) select cart.CartProductsLists).ToList().FirstOrDefault();
                foreach (var cartproduct in list)
                {
                    success = success && Order(OrderId, OUserId, new ProductsOrderList() { ProductId = cartproduct.ProductId, ProductQty = cartproduct.Qty });
                    if (success)
                    {
                        Products.RemoveFromCart(cartproduct.ProductId);
                    }
                    TotalPrice += cartproduct.Qty*(from product in context.Products where (product.Id == cartproduct.ProductId) select product.Price).ToList().FirstOrDefault();
                }
                (from order in context.Orders where (order.Id == OrderId) select order).ToList().FirstOrDefault().OrderTotalAmount = TotalPrice;
                context.SaveChanges();
            }


            if (success)
                return OrderId;
            return new Guid();
        }
        public static Guid GetAddress(Guid UserId, Guid OrderID)
        {
            Guid ShippingID = Guid.NewGuid();
            Guid BillingID = Guid.NewGuid();
            using (var context = new ShoppingCartEntities())
            {
                var shipping = (from address in context.Addresses where ((address.UserId == UserId) && address.IsSelected) select address).ToList().FirstOrDefault();
                var billing = (context.BillingAddresses).ToList().FirstOrDefault();
                context.ShippingAndBillingAddresses.Add(
                    new ShippingAndBillingAddress()
                    {
                        Id = ShippingID,

                        City = shipping.City,
                        LocalityOrStreet = shipping.LocalityOrStreet,
                        FlatNoBuildingName = shipping.FlatNoBuildingName,
                        PinCode = shipping.PinCode,
                        State = shipping.State,
                        LandMark = shipping.LandMark,
                        FirstName = shipping.FirstName,
                        MobileNumber = shipping.MobileNumber,
                        AlternateMobileNumber = shipping.AlternateMobileNumber,
                        // ShippingId=ShippingID,
                        BillingId = BillingID
                    }
                    );
                context.ShippingAndBillingAddresses.Add(
                    new ShippingAndBillingAddress()
                    {
                        Id = BillingID,
                        City = billing.City,
                        LocalityOrStreet = billing.LocalityOrStreet,
                        FlatNoBuildingName = billing.FlatNoBuildingName,
                        PinCode = billing.PinCode,
                        State = billing.State,
                        LandMark = billing.LandMark,
                        FirstName = billing.FirstName,
                        LastName = billing.LastName,
                        MobileNumber = billing.MobileNumber,
                        AlternateMobileNumber = billing.AlternateMobileNumber,
                        // ShippingId = ShippingID,
                        //BillingId = BillingID
                    }
                    );
                context.SaveChanges();
            }
            return ShippingID;
        }
    }
}