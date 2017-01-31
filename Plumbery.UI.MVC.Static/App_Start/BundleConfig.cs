using System.Web;
using System.Web.Optimization;

namespace Plumbery.UI.MVC.Static {
    public class BundleConfig {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/js/scripts")
                .Include("~/js/jquery.js")
                .Include("~/js/jquery-migrate-1.1.1.js")
                .Include("~/js/jquery.easing.1.3.js")
                .Include("~/js/script.js")
                .Include("~/js/superfish.js")
                .Include("~/js/jquery.equalheights.js")
                .Include("~/js/jquery.mobilemenu.js")
                .Include("~/js/jquery.ui.totop.js")
                .Include("~/js/owl.carousel.js")
                .Include("~/js/touchTouch.jquery.js")
                .Include("~/js/TMForm.js")
                .Include("~/js/modal.js"));

            bundles.Add(new ScriptBundle("~/css/styles")
                .Include("~/css/style.css")
                .Include("~/css/contact-form.css")
                .Include("~/css/touchTouch.css")
                .Include("~/css/owl.carousel.css"));

        }
    }
}
