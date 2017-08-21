using System;
using System.Configuration;

namespace Web
{
    public static class Site
    {
        public static string MediaHost = ConfigurationManager.AppSettings["Media"];
    }
}