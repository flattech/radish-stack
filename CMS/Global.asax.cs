using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CMS
{
   
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //Logger.EnsureInitialized();
            //Logger.Info(this, "Application is starting up");
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}