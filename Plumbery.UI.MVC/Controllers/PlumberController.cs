using Microsoft.AspNet.Identity;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using Plumbery.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Controllers
{
    [Authorize]
    public class PlumberController : Controller
    {
        private IPlumberService _plumberService;

        public PlumberController(IPlumberService plumberService) {
            _plumberService = plumberService;
        }
        // GET: Plumber
        public ActionResult Index()
        {
            Supervisor supervisor = _plumberService.GetSupervisor(User.Identity.GetUserId());
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            List<Plumber> plumbers = _plumberService.GetPlumbers().ToList();
            return View(plumbers);
        }

        public ActionResult Create() {
            Supervisor supervisor = _plumberService.GetSupervisor(User.Identity.GetUserId());
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            ViewBag.Users = new SelectList(_plumberService.GetUsers(), "Id", "FullName");
            ViewBag.Warehouses = new SelectList(_plumberService.GetWarehouses(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatePlumberViewModel model) {
            Supervisor supervisor = _plumberService.GetSupervisor(User.Identity.GetUserId());
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (ModelState.IsValid) {
                Plumber plumber = new Plumber {
                    UserId = model.UserId,
                    WarehouseId = model.WarehouseId
                };
                _plumberService.AddPlumber(plumber);

                return RedirectToAction("Index");
            }

            ViewBag.Users = new SelectList(_plumberService.GetUsers(), "Id", "FullName");
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? Id) {
            Supervisor supervisor = _plumberService.GetSupervisor(User.Identity.GetUserId());
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (Id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plumber plumber = _plumberService.GetPlumber(Id);
            if (plumber == null) {
                return HttpNotFound();
            }
            ViewBag.Users = new SelectList(_plumberService.GetUsers(), "Id", "FullName",plumber.UserId);
            return View(plumber);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,WarehouseId")]Plumber plumber) {
            Supervisor supervisor = _plumberService.GetSupervisor(User.Identity.GetUserId());
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (ModelState.IsValid) {
                _plumberService.EditPlumber(plumber);
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList(_plumberService.GetUsers(), "Id", "FullName",plumber.UserId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection collection) {
            Supervisor supervisor = _plumberService.GetSupervisor(User.Identity.GetUserId());
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            string id = collection.Get("PlumberId");
            int Id = Convert.ToInt32(id);
            _plumberService.DeletePlumber(Id);
            return RedirectToAction("Index");
        }
    }
}