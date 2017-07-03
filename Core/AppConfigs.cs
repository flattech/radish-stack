namespace Core
{
    public class AppConfigs
    {
        public static readonly AppConfigs Instance = new AppConfigs();
        
        public ThemeConfig Theme { get; set; }
        public SeoSettings SeoSettings { get; set; }


        private AppConfigs()
        {
            Theme = new ThemeConfig();
            SeoSettings = new SeoSettings();
           // Fill();
        }

        //private void Fill()
        //{
        //    foreach (var config in GlobalConfigs.Entries)
        //    {
        //        switch (config.Key)
        //        {
        //            case (int)ConfigsKey.Theme:
        //                Theme = config.Value.FromJson<ThemeConfig>();
        //                break;
                  
        //        }
        //    }
        //}

        public void Refresh()
        {
           // Fill();
        }
    }
    public class SeoSettings
    {

        public string PageTitleSeparator { get; set; }

        //public PageTitleSeoAdjustment PageTitleSeoAdjustment { get; set; }

        public string DefaultTitle { get; set; }

        public string DefaultMetaKeywords { get; set; }

        public string DefaultMetaDescription { get; set; }

        public bool GenerateProductMetaDescription { get; set; }

        public bool ConvertNonWesternChars { get; set; }

        public bool AllowUnicodeCharsInUrls { get; set; }

        public bool CanonicalUrlsEnabled { get; set; }

        //public WwwRequirement WwwRequirement { get; set; }

        public bool EnableJsBundling { get; set; }

        public bool EnableCssBundling { get; set; }

        public bool TwitterMetaTags { get; set; }

        public bool OpenGraphMetaTags { get; set; }

        public string ReservedUrlRecordSlugs { get; set; }
    }
    public class ThemeConfig
    {
        public ThemeConfig()
        {
            ThemeName = "DefaultClean";
        }
        public bool SupportRtl { get; set; }

        public string ThemeName { get; set; }

    }
    public enum ConfigsKey
    {
        Styling = 500,
        Services = 510,
        Cdn = 520,
        OrganizationInfo = 540,
        Newsletter = 570,
        Analytics = 580,
        Language = 590,
        MediaConfig = 610,
        Misc = 700,
        Theme = 800,
        SeoSettings = 900
    }
}
