using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core;
using Web.Helpers.Themes;

namespace Web
{
    public class MvcApplication : HttpApplication
    {            
        
        protected void Application_Start()
        {
            Logger.EnsureInitialized();
            Logger.Info(this, "Radish Web is starting up");
            //var theme= AppSettings.Instance.Theme;
            //disable "X-AspNetMvc-Version" header name
            MvcHandler.DisableMvcResponseHeader = true;

            //remove all view engines
            ViewEngines.Engines.Clear();
            //except the themeable razor view engine we use
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

           //Registering some regular mvc stuff
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //register custom routes (plugins, etc)
            // var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            // routePublisher.RegisterRoutes(routes);

            //home page
            routes.MapRoute("HomePage",
                "",
                new {controller = "Home", action = "Index"}, new[] {"Web.Controllers"});
          
            //sitemap
            routes.MapRoute("Sitemap",
                "sitemap",
                new {controller = "Common", action = "Sitemap"}, new[] {"Web.Controllers"});


            //robots.txt
            routes.MapRoute("robots.txt",
                "robots.txt",
                new {controller = "Common", action = "RobotsTextFile"},
                new[] {"Web.Controllers"});

            //sitemap (XML)
            routes.MapRoute("sitemap.xml",
                "sitemap.xml",
                new {controller = "Common", action = "SitemapXml"}
                , new[] {"Web.Controllers"});

            //page not found
            routes.MapRoute("PageNotFound",
                "page-not-found",
                new {controller = "Common", action = "PageNotFound"},
                new[] {"Web.Controllers"});

            routes.MapRoute(
                "Post", // Route name
                "{posttype}/post/{id}", // URL with parameters
                new {controller = "Post", action = "Details", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                "Term", // Route name
                "{posttype}/term/{id}", // URL with parameters
                new { controller = "Term", action = "Index", id = UrlParameter.Optional }
                );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Web.Controllers" }
            );
        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
  
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            SetWorkingCulture();
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
            Logger.Fatal(this, "#RadishWeb #Error #Web #Global Method:" + Request.HttpMethod + "----ip :" + ip + "--referrr " + referrr + " Application global error " + url, ex);

        }

        protected void SetWorkingCulture()
        {
            //if (WebHelper.IsStaticResource(this.Request))
            //    return;
       }
    }
}