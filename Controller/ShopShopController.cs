using ShopShop.Models.DataBaseModels;
using ShopShop.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Security;

namespace ShopShop.Controller
{
    public class ShopShopController : ApiController
    {
        [HttpGet]
        public string LoggedUserName()
        {
            return LoggedIn.UserName();
        }
        [HttpGet]
        public string LoggedUserEmailId()
        {
            return LoggedIn.UserEmail();
        }

        public bool login(UserLoginInfo user)
        {
            if (!ModelState.IsValid)
                return false;
            return Login.LoginMember(user.UserName, user.Password, true);
        }
        [HttpGet]
        public bool Logout()
        {
            return LogOut.Logout();
        }
        public Guid GetId()
        {
            return Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
        }
        [HttpGet]
        public string GetRole()
        {
            return Roles.GetRolesForUser(Membership.GetUser().UserName).ToList().FirstOrDefault();
        }
        [HttpGet]
        public List<String> GetCategories()
        {
            return Categories.GetNames();
        }
        public List<ProductCategory> GetCategoriesList()
        {
            return Categories.GetCategories();
        }
        [HttpGet]
        public Guid GetCategoryId(string category)
        {
            return Categories.GetId(category);
        }
        [HttpGet]
        public List<ProxyProduct> GetAllProducts()
        {
            return Products.GetAllProducts();
        }
        [HttpGet]
        public List<ProxyCategory> GetCategoriesWithProducts()
        {
            return Categories.CategoriesIncludingProducts();
        }
        public List<ProxyProduct> GetCartProducts()
        {
            return Products.GetCartProducts();
        }
        public List<Address> GetAllAddresses()
        {
            return Addresses.GetAll();
        }

        [HttpGet]
        public int CartProductsCount()
        {
            return Products.GetCartProductsCount();
        }
        [HttpGet]
        public List<ProxyProduct> GetProduct(Guid ProductId)
        {
            return Products.GetProduct(ProductId);
        }
        [HttpGet]
        public Address GetSelectedAddress()
        {
            return Users.SelectedAddress();
        }
        [HttpPost]
        public bool Registeration(ProxyUserDetail User)
        {
            if (!ModelState.IsValid)
                return false;
            else
            {
                return (Register.Add(User));
            }
        }
        [HttpPost]
        public bool PostCategories(ProductCategory Category)
        {

            return AddCategories.Add(Category);
        }
        public bool PostProducts(Product product)
        {
            return AddProducts.Add(product);
        }
        public bool PostProductToCart(CartProductsList cart)
        {

            return AddToCart.Add(cart);
        }
        public bool PostAddress(Address address)
        {
            return AddAddress.Add(address);
        }
        public Guid PostPayment(ShoppingInformation ShoppingInfo)
        {
            if (!ModelState.IsValid)
                return new Guid();
            return Shopping.Shop(ShoppingInfo);
        }
        public bool PostBillingAddress(Models.DataBaseModels.UserBillingAddress address)
        {
            if (!ModelState.IsValid)
                return false;
            new AddAddress().AddToSession(address);
            return true;
        }
        [HttpPut]
        public bool PutSelectedAddress(Guid AddressId)
        {
            return Users.UpdateSelectedAddress(AddressId);
        }

        [HttpPut]
        public bool PutQuentity(List<CartProductListModel> list)
        {
            AddToCart.UpdateQuentity(list);
            return true;
        }
        [HttpDelete]
        public bool DeleteProductFromCart(Guid Id)
        {

            return Products.RemoveFromCart(Id);
        }
        public bool DeleteAddress(Guid id)
        {
            return Users.DeleteAddress(id);
        }
    }
}
