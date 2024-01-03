using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace ShopShop.Models.Models
{
    public class Products
    {
        public static List<ProxyProduct> GetCartProducts()
        {
            var UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            List<ProxyProduct> list;
            using (var context = new ShoppingCartEntities())
            {
                var Cart = (from cart in context.Carts.Include("CartProductsLists") where (cart.UserId == UserId) select cart).ToList().FirstOrDefault();
                list = (from ProductList in Cart.CartProductsLists
                        select (new ProxyProduct()
                        {
                            Id = ProductList.Product.Id,
                            CategoryId = ProductList.Product.CategoryId,
                            Name = ProductList.Product.Name,
                            Price = ProductList.Product.Price,
                            TotalNumberOfProducts = ProductList.Product.TotalNumberOfProducts,
                            Qty = ProductList.Qty,
                        }
)).ToList();
            }
            return list;
        }
        public static int GetCartProductsCount()
        {
            int CartCount = 0;
            Guid UserId;
            try
            {
                UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());

                using (var context = new ShoppingCartEntities())
                {
                    CartCount = (from cart in context.Carts.Include("CartProductsLists") where (cart.UserId == UserId) select cart).ToList().FirstOrDefault().CartProductsLists.Count;
                }
            }
            catch (Exception) { }
            return CartCount;
        }
        public static List<ProxyProduct> GetProduct(Guid Id)
        {
            List<ProxyProduct> product;
            using (var context = new ShoppingCartEntities())
            {
                product = (from Products in context.Products
                           where (Products.Id == Id)
                           select
new ProxyProduct()
{
Id = Products.Id,
CategoryId = Products.CategoryId,
Name = Products.Name,
Price = Products.Price,
TotalNumberOfProducts = Products.TotalNumberOfProducts,
Qty = 1
}
                           ).ToList();
            }
            return product;
        }
        public static List<ProxyProduct> GetAllProducts()
        {
            List<Product> Products = null;
            using (var context = new ShoppingCartEntities())
            {
                Products = context.Products.ToList();
            }
            List<ProxyProduct> AllProducts = (from product in Products
                                              select (new ProxyProduct()
                                              {
                                                  Id = product.Id,
                                                  CategoryId = product.CategoryId,
                                                  Name = product.Name,
                                                  Price = product.Price,
                                                  TotalNumberOfProducts = product.TotalNumberOfProducts
                                              })).ToList();
            return AllProducts;
        }
        public static bool RemoveFromCart(Guid Id)
        {
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            using (var context = new ShoppingCartEntities())
            {
                //Guid CartId = Guid.Parse((from cart in context.Carts where (cart.UserId == UserId) select cart.Id).ToList().FirstOrDefault().ToString());
                var CartId = (from cart in context.Carts where (cart.UserId == UserId) select cart.Id).FirstOrDefault();
                var product = (from CartProduct in context.CartProductsLists where ((CartProduct.ProductId == Id) && (CartProduct.CartId == CartId)) select CartProduct).ToList().FirstOrDefault();
                context.CartProductsLists.Remove(product);
                try { context.SaveChanges(); } catch (Exception) { return false; }
            }
            return true;
        }
    }

}