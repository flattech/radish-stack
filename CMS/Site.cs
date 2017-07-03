using System;
using System.Configuration;

namespace CMS
{
    public static class Site
    {
        public static string ApiServerUrl = ConfigurationManager.AppSettings["ApiServerUrl"];
        public static string Blob = ConfigurationManager.AppSettings["Blob"];
        public static string UiServerUrl = ConfigurationManager.AppSettings["UiServerUrl"];
        public static bool EnableArea = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableArea"]);

        public static string AreaName
        {
            get
            {
                return  EnableArea ? "/Administration":"";
            }
        }
  
    }
}