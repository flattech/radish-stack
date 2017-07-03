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
           var theme= AppConfigs.Instance.Theme;
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
                            new { controller = "Home", action = "Index" }, new[] { "Web.Controllers" });
            //sitemap
            routes.MapRoute("Sitemap",
                            "sitemap",
                            new { controller = "Common", action = "Sitemap" }, new[] { "Web.Controllers" });


            //robots.txt
            routes.MapRoute("robots.txt",
                            "robots.txt",
                            new { controller = "Common", action = "RobotsTextFile" },
                            new[] { "Web.Controllers" });

            //sitemap (XML)
            routes.MapRoute("sitemap.xml",
                            "sitemap.xml",
                            new { controller = "Common", action = "SitemapXml" }
                            , new[] { "Web.Controllers" });

            //page not found
            routes.MapRoute("PageNotFound",
                            "page-not-found",
                            new { controller = "Common", action = "PageNotFound" },
                            new[] { "Web.Controllers" });

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

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            //log error
            //LogException(exception);
        }

        protected void SetWorkingCulture()
        {
            //if (WebHelper.IsStaticResource(this.Request))
            //    return;
       }
    }
}