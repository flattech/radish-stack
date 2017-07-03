using System.Web.Optimization;

namespace CMS
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/content/js").Include(
              //  "~/Content/Scripts/jquery-1.10.2.js",
                "~/Content/Scripts/jquery-migrate-1.2.1.js",
                "~/Content/Scripts/jquery-ui-1.10.3.custom.js",
                "~/Content/Scripts/bootstrap.js",
                 "~/Content/Scripts/jquery.signalR-2.0.0.js",
                // "~/Content/Scripts/jqx-all.js",
                //"~/Content/Scripts/globalize.culture.ar-LB.js",
                "~/Content/Scripts/twitter-bootstrap-hover-dropdown.js",
                "~/Content/Scripts/jquery.slimscroll.js",
                "~/Content/Scripts/jquery.blockui.js",
                "~/Content/Scripts/jquery.cookie.js",
                "~/Content/Scripts/jquery.flot.js",
                "~/Content/Scripts/jquery.flot.resize.js",
                "~/Content/Scripts/jquery.sparkline.js",
                "~/Content/Scripts/bootbox.js",
                "~/Content/Scripts/select2.js",
                 "~/Content/Scripts/jquery.gritter.js",
                "~/Content/Scripts/bootstrap-datepicker.js"
                //"~/Content/Scripts/jqxgrid/jqxcore.js",
                //"~/Content/Scripts/jqxgrid/jqxchart.js",
                //"~/Content/Scripts/jqxgrid/jqxdata.js",
               
                
                //"~/Content/Scripts/jqxgrid/jqxbuttons.js",
                //"~/Content/Scripts/jqxgrid/jqxdatetimeinput.js",
                //"~/Content/Scripts/jqxgrid/jqxcalendar.js",
                //"~/Content/Scripts/jqxgrid/jqxcheckbox.js",
                //"~/Content/Scripts/jqxgrid/jqxscrollbar.js",
                //"~/Content/Scripts/jqxgrid/jqxmenu.js",
                //"~/Content/Scripts/jqxgrid/jqxlistbox.js",
                //"~/Content/Scripts/jqxgrid/jqxdropdownlist.js",
                //"~/Content/Scripts/jqxgrid/jqxgrid.js",                
                //"~/Content/Scripts/jqxgrid/jqxgrid.selection.js",
                // "~/Content/Scripts/jqxgrid/jqxgrid.edit.js ",
                //"~/Content/Scripts/jqxgrid/jqxgrid.columnsresize.js",
                //"~/Content/Scripts/jqxgrid/jqxgrid.filter.js",
                //"~/Content/Scripts/jqxgrid/jqxgrid.sort.js",
                //"~/Content/Scripts/jqxgrid/jqxgrid.pager.js",
                //"~/Content/Scripts/jqxgrid/jqxgrid.grouping.js",
                //"~/Content/Scripts/jqxgrid/jqxgrid.aggregates.js"
                ));

            bundles.Add(new StyleBundle("~/Content/globalcss").Include(
                "~/Content/plugins/font-awesome/font-awesome.css",
                "~/Content/plugins/simple-line-icons/simple-line-icons.css",
                "~/Content/plugins/bootstrap/css/bootstrap.css",
                "~/Content/plugins/uniform/css/uniform.default.css",
                "~/Content/plugins/bootstrap-switch/css/bootstrap-switch.css"
                ));
  
            bundles.Add(new StyleBundle("~/Content/themestyles").Include(
                 "~/Content/css/components.css",
                 "~/Content/css/plugins.css",
                 "~/Content/css/layout.css",
                 "~/Content/css/themes/darkblue.css",
                 "~/Content/css/custom.css",
                 "~/Content/css/plugins.css"
                     ));
            bundles.Add(new ScriptBundle("~/content/jqxgrid").Include(
         "~/Content/Scripts/jqxgrid/jqxcore.js",
            "~/Content/Scripts/jqxgrid/jqxchart.js",
            "~/Content/Scripts/jqxgrid/jqxdata.js",
            "~/Content/Scripts/jqxgrid/jqxbuttons.js",
            "~/Content/Scripts/jqxgrid/jqxdatetimeinput.js",
            "~/Content/Scripts/jqxgrid/jqxcalendar.js",
            "~/Content/Scripts/jqxgrid/jqxcheckbox.js",
            "~/Content/Scripts/jqxgrid/jqxscrollbar.js",
            "~/Content/Scripts/jqxgrid/jqxmenu.js",
            "~/Content/Scripts/jqxgrid/jqxlistbox.js",
            "~/Content/Scripts/jqxgrid/jqxdropdownlist.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.selection.js",
             "~/Content/Scripts/jqxgrid/jqxgrid.edit.js ",
            "~/Content/Scripts/jqxgrid/jqxgrid.columnsresize.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.filter.js",
          //  "~/Content/Scripts/jqxgrid/jqxgrid.storage.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.sort.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.pager.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.grouping.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.aggregates.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.storage.js",
            "~/Content/Scripts/jqxgrid/jqxdata.export.js",
            "~/Content/Scripts/jqxgrid/jqxgrid.export.js"
           ));
            bundles.Add(new StyleBundle("~/Content/globalcssrtl").Include(
               "~/Content/Styles/Global/font-awesome.css",
               "~/Content/Fonts/font.css",
               "~/Content/Styles/Global/bootstrap-rtl.css",
               "~/Content/Styles/Global/uniform.default.css"
               ));

            bundles.Add(new StyleBundle("~/Content/themestylesrtl").Include(
               "~/Content/Styles/Theme/select2_metro.css",
               "~/Content/Styles/Theme/style-metronic-rtl.css",
               "~/Content/Styles/Theme/style-rtl.css",
               "~/Content/Styles/Theme/style-responsive-rtl.css",
               "~/Content/Styles/Theme/plugins-rtl.css",
               "~/Content/Styles/Theme/jquery.gritter-rtl.css",
               "~/Content/Styles/Theme/default-rtl.css",
               "~/Content/Styles/Theme/custom-rtl.css",
               "~/Content/Styles/Pages/tasks.css",
               "~/Content/Styles/Theme/lock.css",
               "~/Content/Styles/Theme/jqx.base.css",
               "~/Content/Styles/Theme/jqx.arctic.css",
               "~/Content/Styles/Theme/datepicker.css",
               "~/Content/Styles/Site.css"
                   ));
        }
    }
}