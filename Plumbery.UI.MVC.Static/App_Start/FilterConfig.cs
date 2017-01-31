using System.Web;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Static {
    public class FilterConfig {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
