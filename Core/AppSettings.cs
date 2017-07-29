using Core.Extentions;

namespace Core
{
    public class AppSettings
    {
        public static readonly AppSettings Instance = new AppSettings();

        public ThemeConfig Theme { get; set; }
        public SeoSettings SeoSettings { get; set; }


        private AppSettings()
        {
            Theme = new ThemeConfig();
            SeoSettings = new SeoSettings();
            Fill();
        }

        private void Fill()
        {
            foreach (var config in Pool.Instance.Settings.GetAll())
            {
                switch (config.Type)
                {
                    case (int)SettingsType.Theme:
                        Theme = config.Value.FromJson<ThemeConfig>();
                        break;
                }
            }
        }

        
        public void Refresh()
        {
            Fill();
        }
    }
    public class SeoSettings
    {

        public string Site { get; set; }
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
    public enum SettingsType
    {
        Theme = 10,
        Seo = 20
    }
}
