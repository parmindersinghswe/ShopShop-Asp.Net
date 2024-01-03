using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
namespace ShopShop.Models.Models
{
    public class Login
    {
        public static bool LoginMember(string UserName, string Password, bool RememberMe)
        {
            if (System.Web.Security.Membership.ValidateUser(UserName, Password))
            {
                var userid = HttpContext.Current.User.Identity.Name;
                FormsAuthentication.SetAuthCookie(UserName, true);

                if (RememberMe)
                {
                    System.Web.Security.FormsAuthenticationTicket authTicket = new System.Web.Security.FormsAuthenticationTicket(UserName, true, 2 * 60);
                    string encryptedTicket = System.Web.Security.FormsAuthentication.Encrypt(authTicket);
                    HttpCookie cookie = new HttpCookie(System.Web.Security.FormsAuthentication.FormsCookieName, encryptedTicket);
                    cookie.Expires = authTicket.Expiration;
                    HttpContext.Current.Response.Cookies.Set(cookie);

                }
                else
                {

                    System.Web.Security.FormsAuthentication.SetAuthCookie(UserName, false);
                }
                return true;
            }
            else
                return false;
        }
    }
}