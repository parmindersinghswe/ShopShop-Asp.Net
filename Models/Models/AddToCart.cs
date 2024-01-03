using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
namespace ShopShop.Models.Models
{
    public class AddToCart
    {
        public static bool Add(CartProductsList cpl)
        {
            cpl.Id = Guid.NewGuid();
            var UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            using (var context = new ShoppingCartEntities())
            {
                Guid CartId = (from cart in context.Carts where (cart.UserId == UserId) select cart.Id).ToList().FirstOrDefault();
                int product = (from products in context.CartProductsLists where ((products.ProductId == cpl.ProductId) && (products.CartId == CartId)) select products).ToList().Count;
                if (product < 1)
                {
                    cpl.CartId = CartId;
                    context.CartProductsLists.Add(cpl);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception) { return false; }
                }
                else return false;
            }
            return true;

        }
        public static bool UpdateQuentity(List<CartProductListModel> list)
        {
            var UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            using (var context = new ShoppingCartEntities())
            {
                var cart = (from carts in context.Carts where (carts.UserId == UserId) select carts).ToList().FirstOrDefault();

                foreach (var p in list)
                {
                    try
                    {
                        var p1 = (from products in context.CartProductsLists where ((products.ProductId == p.ProductId) && (products.CartId == cart.Id)) select products).ToList().FirstOrDefault();
                        p1.Qty = p.Qty;
                    }
                    catch (Exception) { }
                }
                context.SaveChanges();
            }
            return true;
        }
    }
    public class CartProductListModel
    {
        public Guid ProductId { get; set; }
        public int Qty { get; set; }
    }
}