using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ShopShop.Models.DataBaseModels
{
    public class ShoppingCartEntities : DbContext
    {
        public ShoppingCartEntities() : base("name=ShopShop")
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProductsList> CartProductsLists { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserBillingAddress> BillingAddresses { get; set; }
        public DbSet<ShippingAndBillingAddress> ShippingAndBillingAddresses { get; set; }
        public DbSet<ProductsOrderList> ProductsOrderLists { get; set; }
        public DbSet<User> Users { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
    }
}