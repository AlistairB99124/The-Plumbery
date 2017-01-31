using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Controllers {
    public class HomeController : Controller {

        private IHomeService _homeService;

        public HomeController(IHomeService homeService) {
            this._homeService = homeService;
        }
        /// <summary>
        /// Load Home Page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            return View();
        }
        
        /*OUT OF SCOPE*/
        //public ActionResult Search(string q) {
        //    string query = q;
        //    string[] array = query.Split(' ');
        //    List<string> keywords = array.ToList();

        //    ViewBag.Inventory = _homeService.SearchInventory(keywords);
        //    ViewBag.Supervisors = _homeService.SearchSupervisors(keywords);
        //    ViewBag.Plumbers = _homeService.SearchPlumbers(keywords);
        //    ViewBag.Sites = _homeService.SearchSites(keywords);
        //    ViewBag.Warehouse = _homeService.SearchWarehouse(keywords);
        //    ViewBag.TimeSheets = _homeService.SearchTimeSheets(keywords);
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Search(FormCollection collection) {
        //    string q = collection.Get("q");
        //    return RedirectToAction("Search", "Home", new { q = q });
        //}
    }
}