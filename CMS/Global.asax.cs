using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Core;

namespace CMS
{
   
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Logger.EnsureInitialized();
            Logger.Info(this, "Radish CMS is starting up");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Logger.EnsureInitialized();
            //Logger.Info(this, "Application is starting up");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        void Application_Error(object sender, EventArgs e)
        {
            var url = Request.Url.ToString();
            var referrr = "";
            if (Request.UrlReferrer != null)
                referrr = Request.UrlReferrer.ToString();
            var ip = "";
            if (Request.UserHostAddress != null)
                ip = Request.UserHostAddress;
            if (url.EndsWith("favicon.ico")) return;
            var lastError = Server.GetLastError();
            if (lastError == null) return;
            var ex = lastError.GetBaseException();
            //if (ex is HttpRequestValidationException)
            //{
            //    Response.Clear();
            //    Server.ClearError();
            //    Response.Write(@"<html><head></head><body></body></html>");
            //    Response.End();
            //}
            Logger.Fatal(this, "#RadishCMS #Error #CMS #Global Method:" + Request.HttpMethod + "----ip :" + ip + "--referrr " + referrr + " Application global error " + url, ex);

        }
    }
}