using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult InternalServer() {
            return View();
        }

        public ActionResult NotFound() {
            return View();
        }
    }
}