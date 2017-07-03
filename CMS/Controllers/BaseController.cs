using System.IO;
using System.Web.Mvc;
using CMS.Helpers;
using Core;
using Core.Extentions;
using Core.Repositories;
using Core.Repositories.Enums;


namespace CMS.Controllers
{
    public abstract class BaseController : Controller
    {
        protected Pool UOW = Pool.Instance;
        protected UserSession UserSession = new UserSession();
        protected string QueryLike(string column, string value)
        {
            return value.IsNotNullOrEmpty() ? column + " LIKE N'%" + value + "%'" : "";
        }
        protected string Query(string column, string value)
        {
            return value.IsNotNullOrEmpty() && value != "0" ? column + " = '" + value + "'" : "";
        }
        protected BaseController()
        {
            ViewBag.CurrentSection = "";
            ViewBag.NewDesign = false;
            var user = UserSession.GetCurrentUser();
            ViewBag.IsAdmin = false;
            if (user != null)
            {
                User = user;
                IsAdmin = ViewBag.IsAdmin = user.RoleEnum == UserRoleEnum.Admin;
            }

        }

        public User User { get; set; }
        public bool IsAdmin { get; set; }
        protected void AlertSuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        private string StorageRoot
        {
            get { return Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/content/uploaded/")); } //Path should! always end with '/'
        }
        protected void AlertErrorMessage(string message)
        {
            TempData["ErrorMessage"] = "An error has occured : " + message;
        }
        public User CurrentUser
        {
            get
            {
                return UserSession.GetCurrentUser();
            }
        }
    }

}