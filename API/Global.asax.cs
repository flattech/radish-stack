using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Core;

namespace API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Logger.EnsureInitialized();
            Logger.Info(this, "Radish API is starting up");
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Insert(0, new QueryStringMapping("json", "true", "application/json"));

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
            Logger.Fatal(this, "#RadishAPI #Error #API #Global Method:" + Request.HttpMethod + "----ip :" + ip + "--referrr " + referrr + " Application global error " + url, ex);

        }
    }
}
