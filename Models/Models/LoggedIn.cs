using ShopShop.Models.DataBaseModels;
using System;
using System.Web.Security;
using System.Linq;
namespace ShopShop.Models.Models
{
    public class LoggedIn
    {
        public static string UserName()
        {
            var islogin = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            string Name = "";
            if (islogin)
            {
                Guid UserId=Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
                using (var context = new ShoppingCartEntities())
                {
                    Name = (from user in context.Users where (user.Id==UserId) select(user.FirstName+" "+user.LastName)).ToList().FirstOrDefault();
                }
            }
            return Name;
        }
        public static string UserEmail()
        {
            var islogin = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            string Email = "";
            if (islogin)
                Email = Membership.GetUser().UserName;
            return Email;
        }
    }
}