using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using Plumbery.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Plumbery.UI.MVC.Models;

namespace Plumbery.UI.MVC.Controllers {
    public class WarehouseController : Controller {
        /// <summary>
        /// Warehouse service
        /// </summary>
        private IWarehouseService _warehouseService;
        /// <summary>
        /// Initialise Warehouse Services
        /// </summary>
        /// <param name="WarehouseService"></param>
        public WarehouseController(IWarehouseService warehouseService) {
            this._warehouseService = warehouseService;
        }

        /// <summary>
        /// Get list of all Warehouses
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            List<Warehouse> Warehouses = _warehouseService.GetAll().ToList();
            return View(Warehouses);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateWarehouseViewModel model) {
            if (ModelState.IsValid) {
                Warehouse Warehouse = new Warehouse {
                    Name = model.Name
                };
                var WarehouseResult = await _warehouseService.Add(Warehouse);
                if (WarehouseResult == 1) {
                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "Could not add Warehouse";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? Id) {
            if (Id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Warehouse Warehouse = _warehouseService.Get(Id);
            if (Warehouse == null) {
                return HttpNotFound();
            }
            return View(Warehouse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Warehouse model) {

            Warehouse Warehouse = _warehouseService.Get(model.Id);
            if (ModelState.IsValid) {
                Warehouse.Name = model.Name;
                int WarehouseResult = await _warehouseService.Edit(Warehouse);
                if (WarehouseResult == 1) {
                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "Could not edit Warehouse";
                    return View(model);
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(FormCollection collection) {
            try {
                string idString = collection.Get("Id");
                int Id = Convert.ToInt32(idString);
                Warehouse Warehouse = _warehouseService.Get(Id);
                int result = await _warehouseService.Delete(Warehouse);
                if (result > 0) {
                    return RedirectToAction("Index");
                } else {
                    ViewBag.ErrorMessage = "Could not delete Warehouse";
                    return RedirectToAction("Index");
                }
            } catch {
                return RedirectToAction("Index");
            }
        }
    }
}