using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ShopShop.Models.Models
{
    public class LogOut
    {
        public static bool Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Roles.DeleteCookie();
            }
            catch (Exception) { return false; }
            return true;
        }
    }
}