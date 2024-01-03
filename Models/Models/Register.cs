using ShopShop.Models.DataBaseModels;
using System;
using System.Web.Security;

namespace ShopShop.Models.Models
{
    public class Register
    {
        public static bool Add(ProxyUserDetail User)
        {
            MembershipCreateStatus createStatus;
            MembershipUser user;
            try
            {
                user = Membership.CreateUser(User.Email, User.Password, User.Email, User.SecurityQuestion, User.SecurityAnswer, true, out createStatus);
            }
            catch (Exception e) { var x = e.Message; return false; }

            switch (createStatus)
            {
                case MembershipCreateStatus.Success:

                    using (ShoppingCartEntities context = new ShoppingCartEntities())
                    {
                        context.Users.Add(new User()
                        {
                            Id = Guid.Parse(user.ProviderUserKey.ToString()),
                            FirstName = User.FirstName,
                            LastName = User.LastName,
                            MobileNumber = User.MobileNumber,
                        });
                        context.Carts.Add(new Cart()
                        {
                            Id = Guid.NewGuid(),
                            UserId = Guid.Parse(user.ProviderUserKey.ToString())
                        }
                        );
                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception)
                        {
                            Membership.DeleteUser(User.Email);
                            return false;
                        }
                        Roles.AddUserToRole(User.Email, "User");
                        return (Login.LoginMember(User.Email, User.Password, false));
                    }


                //case MembershipCreateStatus.DuplicateUserName:
                //   // MessageBox.Show("The user with the same Name already exists!");
                //    break;

                //case MembershipCreateStatus.DuplicateEmail:
                //   // MessageBox.Show("The user with the same email address already exists!");
                //    break;
                //case MembershipCreateStatus.InvalidEmail:
                //   // MessageBox.Show("The email address you provided is invalid.");
                //    break;
                //case MembershipCreateStatus.InvalidAnswer:
                //    //MessageBox.Show("The security answer was invalid.");
                //    break;
                //case MembershipCreateStatus.InvalidPassword:
                //    //MessageBox.Show("The password you provided is invalid. It must be 7 characters long and have at least 1 special character.");
                //    break;
                default:
                    //MessageBox.Show("There was an unknown error; the student account was NOT created.");
                    //break;
                    return false;
            }

        }
    }
}