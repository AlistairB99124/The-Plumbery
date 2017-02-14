using System.Web;
using System.Web.Optimization;

namespace Plumbery.UI.MVC {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/bootstrap.css")
                .Include("~/Content/css/select2.min.css")
                .Include("~/Content/css/datepicker3.css")
                .Include("~/Content/css/AdminLTE.min.css")
                .Include("~/Content/css/skins/skin-blue.css")
                .Include("~/Content/css/Styles.css"));

            bundles.Add(new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/bootstrap/bootstrap.min.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.min.js")
                .Include("~/Content/js/plugins/moment/moment.min.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Content/js/plugins/icheck/icheck.min.js")
                .Include("~/Content/js/plugins/select2/select2.full.min.js")
                .Include("~/Content/js/app.min.js")
                .Include("~/Content/js/init.js"));

#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
