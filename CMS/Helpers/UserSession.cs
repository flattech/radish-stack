using System;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using Core;
using Core.Repositories;

namespace CMS.Helpers
{
    public class UserSession
    {
        private Pool UOW=Pool.Instance;
        public void SignIn(string email, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "Email");

            FormsAuthentication.SetAuthCookie(email, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public User GetUser(string s, string pass)
        {
            var user = UOW.Users.GetByUsername(s);
            if (user == null || !user.ValidatePassword(pass))
                return null;
            return user;
        }

        public User GetCurrentUser()
        {
            bool isWebHosed = System.Web.HttpContext.Current != null;
            IIdentity identity = isWebHosed ? System.Web.HttpContext.Current.User.Identity : Thread.CurrentPrincipal.Identity;
            if (!identity.IsAuthenticated)
                return null;

            User current = null;
            if (isWebHosed)
                current = System.Web.HttpContext.Current.Items["CurrentUser"] as User;
            else
                current = new User { Username = identity.Name };
            if (current == null)
            {
                var name = identity.Name;
                current = UOW.Users.GetByUsername(name);
                if (isWebHosed) System.Web.HttpContext.Current.Items["CurrentUser"] = current;
            }
            BlowUpIfUserCannotLogin(current);
            return current;
        }


        private void BlowUpIfUserCannotLogin(User user)
        {
            if (user == null && System.Web.HttpContext.Current != null)
            {
                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    SignOut();
                    System.Web.HttpContext.Current.Response.Redirect("/", true);
                }
                else
                    throw new InvalidCredentialException("That User doesn't exist or is not valid.");
            }
        }

    }
}