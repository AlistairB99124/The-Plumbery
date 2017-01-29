using Microsoft.AspNet.Identity;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Controllers
{
    [Authorize]
    public class SupervisorController : Controller
    {
        private ISupervisorService _supervisorService;

        public SupervisorController(ISupervisorService supervisorService) {
            _supervisorService = supervisorService;
        }


        // GET: Supervisor
        public ActionResult Index() {
            Supervisor currentSupervisor = _supervisorService.GetSupervisor(User.Identity.GetUserId());
            if (currentSupervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            var supervisors = _supervisorService.GetAll();
            return View(supervisors);
        }
        public ActionResult Create(string id) {
            Supervisor currentSupervisor = _supervisorService.GetSupervisor(User.Identity.GetUserId());
            if (currentSupervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (id == null) {
                ViewBag.Users = new SelectList(_supervisorService.GetUsers(), "Id", "FullName");
                return View();
            }else {
                ViewBag.Users = new SelectList(_supervisorService.GetUsers(), "Id", "FullName",id);
                return View();
            }
               
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId")] Supervisor supervisor) {
            Supervisor currentSupervisor = _supervisorService.GetSupervisor(User.Identity.GetUserId());
            if (currentSupervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (ModelState.IsValid) {
                _supervisorService.AddSupervisor(supervisor);
                return RedirectToAction("Index");
            }

            ViewBag.Users = new SelectList(_supervisorService.GetUsers(), "Id", "FullName");
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? Id) {
            Supervisor currentSupervisor = _supervisorService.GetSupervisor(User.Identity.GetUserId());
            if (currentSupervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (Id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supervisor Supervisor = _supervisorService.GetSupervisor(Id);
            if (Supervisor == null) {
                return HttpNotFound();
            }
            ViewBag.Users = new SelectList(_supervisorService.GetUsers(), "Id", "FullName");
            return View(Supervisor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,WarehouseId")]Supervisor Supervisor) {
            Supervisor currentSupervisor = _supervisorService.GetSupervisor(User.Identity.GetUserId());
            if (currentSupervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (ModelState.IsValid) {
                _supervisorService.EditSupervisor(Supervisor);
                return RedirectToAction("Index");
            }
            ViewBag.Users = new SelectList(_supervisorService.GetUsers(), "Id", "FullName");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(FormCollection collection) {
            Supervisor currentSupervisor = _supervisorService.GetSupervisor(User.Identity.GetUserId());
            if (currentSupervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            string id = collection.Get("SupervisorId");
            int Id = Convert.ToInt32(id);
            _supervisorService.DeleteSupervisor(Id);
            return RedirectToAction("Index");
        }
    }
}