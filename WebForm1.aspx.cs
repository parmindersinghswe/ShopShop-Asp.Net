using ShopShop.Models.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShopShop
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var context = new ShoppingCartEntities())
            {
                context.Addresses.ToList();
                context.Carts.ToList();
                context.CartProductsLists.ToList();
                context.Images.ToList();
                context.ProductCategories.ToList();
                context.ProductsOrderLists.ToList();
                context.Orders.ToList();
                context.Users.ToList();
                context.BillingAddresses.ToList();
                context.ShippingAndBillingAddresses.ToList();
                Response.Write("Created");
            }
            Roles.CreateRole("Admin");
            Roles.CreateRole("User");



            // Response.Write("User is:"+LoggedIn.User());
            //MembershipCreateStatus status;
            //Membership.CreateUser("parmisingh12@gmail.com", "parminder.", "parmisingh12@gmail.com", "hello", "hi", true, out status);
            //Membership.CreateUser("sarvesh.singh@daffodilsw.com", "parminder.", "sarvesh.singh@daffodilsw.com", "hello", "hi", true, out status);
            //Roles.CreateRole("Admin");
            //Roles.CreateRole("Customer");
            //Roles.AddUserToRole("parmisingh12@gmail.com", "Admin");
            //Roles.AddUserToRole("sarvesh.singh@daffodilsw.com", "Customer");
            //Response.Write("Crested");
            //using (var context = new ShoppingCartEntities())
            // {
            // Guid id = Guid.NewGuid();
            // Response.Write(id);

            //context.Carts.Add(new Cart() { CartId = 1 });
            //context.Addresses.Add(new Address() { UserId = new Guid("1834B643-14D8-4225-B85E-CB763883DFB6"), UserAddress = "manvi",});

            //   context.Users.Add(new User() {Id = new Guid("E14B2280-FBF5-4E15-BC00-AAFC575C0993"),UserName="Parminder Singh",MobileNumber="9803857582"});
            //    context.Users.Add(new User() { Id = new Guid("6637A950-B117-4477-BD45-32A417EF08D1"), UserName = "Sarvesh Singh", MobileNumber = "9898989898"});
            //  context.ProductCategories.Add(new ProductCategory() { Id=Guid.NewGuid(),Name = "Shoes", NumberOfProducts = 1 });
            //    context.ProductCategories.Add(new ProductCategory() { Id = Guid.NewGuid(), Name = "Cloths", NumberOfProducts = 1 });
            //   context.Products.Add(new Product() {Id=Guid.NewGuid(), CategoryId = context.ProductCategories.ToList().FirstOrDefault().Id, Name = "Shoe One", Price = 500 });
            //  context.Products.Add(new Product() {Id=Guid.NewGuid(), CategoryId = context.ProductCategories.ToList().FirstOrDefault().Id, Name = "Shoe Two", Price = 900 });
            //    context.Products.Add(new Product() { Id=Guid.NewGuid(),CategoryId = context.ProductCategories.ToList().LastOrDefault().Id, Name = "Cloth One", Price = 800 });

            //    context.Carts.Add(new Cart() { Id = Guid.NewGuid(), UserId = context.Users.ToList().FirstOrDefault().Id });
            //     context.Carts.Add(new Cart() { Id = Guid.NewGuid(), UserId = context.Users.ToList().LastOrDefault().Id });

            // context.Users.ToList().FirstOrDefault().Cart.
            //  context.CartProductsLists.Add(new CartProductsList() {Id=Guid.NewGuid(), ProductId = context.Products.ToList()[1].Id, CartId = context.Carts.ToList().LastOrDefault().Id });
            //   context.CartProductsLists.Add(new CartProductsList() { Id = Guid.NewGuid(), ProductId = context.Products.ToList().LastOrDefault().Id, CartId = context.Carts.ToList().FirstOrDefault().Id });

            // User user = context.Users.ToList().FirstOrDefault();
            //   var products = user.Cart.CartProductsLists.ToList();
            //  foreach(var x in products)
            //  {
            //        Response.Write("Product Name=" + x.Product.Name + ",Price=" + x.Product.Price);
            //  }
            //    context.SaveChanges();
            // var user = context.Users.Include("Addresses");
            //  var user=context.Users.ToList().FirstOrDefault();
            //var user = context.Users.Include("Cart").Include("Cart.CartProductsLists").Include("Cart.CartProductsLists.Product").Include("Address").ToList();
            //context.Orders.Add(new Order() { UserId = new Guid("1834B643-14D8-4225-B85E-CB763883DFB6"), Address = "manvi", OrderDate = new DateTime(2018,9,12),Status="Just Ordered",ReceiveDate=new DateTime(2018,9,25)});
            // context.ProductsOrderLists.Add(new ProductsOrderList() { OrderId = 1, Price = 1, ProductId = 1 });
            //    var order = context.Users.Include("Orders.ProductOrderLists.Product").ToList();
            //   var order1 = context.Users.Include("Orders.ProductOrderLists.Product").Include("Cart.CartProductsLists.Product").Include("Address").Include("aspnet_Users").ToList();
            //  Response.Write("Ok");
            // context.SaveChanges();
            //}
            // Membership.DeleteUser("sarvesh.singh@daffodilsw.com");
            // Membership.DeleteUser("zaid.khan@daffodilsw.com");
            //Response.Write("Ok");
        }
    }
}