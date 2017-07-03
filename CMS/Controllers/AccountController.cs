using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using Core.Extentions;

namespace CMS.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string returnUrl = "")
        {
            if (Request.IsAuthenticated)
                RedirectToAction("Index", "Home");
            if (Request.IsAjaxRequest())
                return PartialView("_login", new UserLoginModel { UserName = "", Password = "" });
            return View(new UserLoginModel());
        }
     
        public ActionResult LogOff()
        {
            UserSession.SignOut();
            return RedirectToAction("Login");
        }
      
        [HttpPost]
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var u = model.UserName.CleanSql();
                var p = model.Password.CleanSql();
                var user = UserSession.GetUser(u,p);
                if (user != null)
                {
                    UserSession.SignIn(model.UserName, true);
                    if (Request.IsAjaxRequest())
                        return PartialView("Result");
                   // return RedirectToAction("Index", "Home",new{area=Site.EnableArea?"admin":null});
                    return RedirectToAction("Index", "Home");
                }
                model.LoginError = true;
               ModelState.AddModelError("", "Incorrect username or password!");
            }
            if (Request.IsAjaxRequest())
                return PartialView("_login", model);
            return View(model);
        }
        public class UserLoginModel
        {

            [Required]
            public string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public bool LoginError { get; set; }

        }
      
    }
}
