#region Using...

using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Core;

#endregion

namespace Web.Helpers
{
    public delegate LocalizedString Localizer(string text, params object[] args);
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {

        private Localizer _localizer;


        /// <summary>
        /// Get a localized resources
        /// </summary>
        public Localizer T
        {
            get
            {
                if (_localizer == null)
                {
                    //null localizer
                    //_localizer = (format, args) => new LocalizedString((args == null || args.Length == 0) ? format : string.Format(format, args));

                    //default localizer
                    _localizer = (format, args) =>
                                     {
                                         var resFormat = AppsResources.Instance.Get(format);
                                         if (string.IsNullOrEmpty(resFormat))
                                         {
                                             return new LocalizedString(format);
                                         }
                                         return
                                             new LocalizedString((args == null || args.Length == 0)
                                                                     ? resFormat
                                                                     : string.Format(resFormat, args));
                                     };
                }
                return _localizer;
            }
        }

        //public IWorkContext WorkContext
        //{
        //    get
        //    {
        //        return _workContext;
        //    }
        //}

        public override void InitHelpers()
        {
            base.InitHelpers();

           // _localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            //_workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        public HelperResult RenderWrappedSection(string name, object wrapperHtmlAttributes)
        {
            Action<TextWriter> action = delegate(TextWriter tw)
                                {
                                    var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(wrapperHtmlAttributes);
                                    var tagBuilder = new TagBuilder("div");
                                    tagBuilder.MergeAttributes(htmlAttributes);

                                    var section = RenderSection(name, false);
                                    if (section != null)
                                    {
                                        tw.Write(tagBuilder.ToString(TagRenderMode.StartTag));
                                        section.WriteTo(tw);
                                        tw.Write(tagBuilder.ToString(TagRenderMode.EndTag));
                                    }
                                };
            return new HelperResult(action);
        }

        public HelperResult RenderSection(string sectionName, Func<object, HelperResult> defaultContent)
        {
            return IsSectionDefined(sectionName) ? RenderSection(sectionName) : defaultContent(new object());
        }

        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }

        /// <summary>
        /// Return a value indicating whether the working language and theme support RTL (right-to-left)
        /// </summary>
        /// <returns></returns>
        public bool ShouldUseRtlTheme()
        {
            var supportRtl = AppConfigs.Instance.Theme.SupportRtl;
            //_workContext.WorkingLanguage.Rtl;
            //if (supportRtl)
            //{
            //    //ensure that the active theme also supports it
            //    var themeProvider = EngineContext.Current.Resolve<IThemeProvider>();
            //    var themeContext = EngineContext.Current.Resolve<IThemeContext>();
            //    supportRtl = themeProvider.GetThemeConfiguration(themeContext.WorkingThemeName).SupportRtl;
            //}
            return supportRtl;
        }

        /// <summary>
        /// Gets a selected tab index (used in admin area to store selected tab index)
        /// </summary>
        /// <returns>Index</returns>
        public int GetSelectedTabIndex()
        {
            //keep this method synchornized with
            //"SetSelectedTabIndex" method of \Administration\Controllers\BaseNopController.cs
            int index = 0;
            string dataKey = "nop.selected-tab-index";
            if (ViewData[dataKey] is int)
            {
                index = (int)ViewData[dataKey];
            }
            if (TempData[dataKey] is int)
            {
                index = (int)TempData[dataKey];
            }

            //ensure it's not negative
            if (index < 0)
                index = 0;

            return index;
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic>
    {
    }
    public class LocalizedString : MarshalByRefObject, IHtmlString
    {
        private readonly string _localized;
        private readonly string _scope;
        private readonly string _textHint;
        private readonly object[] _args;

        public LocalizedString(string localized)
        {
            _localized = localized;
        }

        public LocalizedString(string localized, string scope, string textHint, object[] args)
        {
            _localized = localized;
            _scope = scope;
            _textHint = textHint;
            _args = args;
        }

        public static LocalizedString TextOrDefault(string text, LocalizedString defaultValue)
        {
            if (string.IsNullOrEmpty(text))
                return defaultValue;
            return new LocalizedString(text);
        }

        public string Scope
        {
            get { return _scope; }
        }

        public string TextHint
        {
            get { return _textHint; }
        }

        public object[] Args
        {
            get { return _args; }
        }

        public string Text
        {
            get { return _localized; }
        }

        public override string ToString()
        {
            return _localized;
        }

        public string ToHtmlString()
        {
            return _localized;
        }

        public override int GetHashCode()
        {
            var hashCode = 0;
            if (_localized != null)
                hashCode ^= _localized.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            var that = (LocalizedString)obj;
            return string.Equals(_localized, that._localized);
        }

    }
}