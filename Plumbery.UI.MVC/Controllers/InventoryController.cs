using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using Microsoft.AspNet.Identity;

namespace Plumbery.UI.MVC.Controllers
{
    [Authorize]
    public class InventoryController : Controller
    {
        private IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService) {
            this._inventoryService = inventoryService;
        }        

        public ActionResult Index() {
            string userId = User.Identity.GetUserId();
            Plumber plumber = _inventoryService.GetPlumber(userId);
            Supervisor supervisor = _inventoryService.GetSupervisor(userId);
            List<Inventory> inventory = null;
            if (plumber == null)
                inventory = _inventoryService.ListAll().ToList();
            else
                inventory = _inventoryService.GetInventoryByWarehouse(plumber.WarehouseId).ToList();

            ViewBag.Plumber = plumber;
            ViewBag.Supervisor = supervisor;
            return View(inventory);
        }

        public ActionResult ControlIndex() {
            string userId = User.Identity.GetUserId();
            Supervisor supervisor = _inventoryService.GetSupervisor(userId);
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }

            List<Inventory> inventory = _inventoryService.GetAll().ToList();
            
            return View(inventory);
        }

        [HttpGet]
        public ActionResult UpdateInventory() {
            string userId = User.Identity.GetUserId();
            Supervisor supervisor = _inventoryService.GetSupervisor(userId);
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            ViewBag.Modified = TempData["Modified"];
            ViewBag.Added = TempData["Added"];
            ViewBag.PlumberSelect = new SelectList(_inventoryService.GetPlumberUsers(), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public ActionResult UpdateInventory(HttpPostedFileBase file, FormCollection collection) {
            string userId = User.Identity.GetUserId();
            Supervisor supervisor = _inventoryService.GetSupervisor(userId);
            if (supervisor == null) {
                return RedirectToAction("Login", "Account", null);
            }
            if (file != null) {
                string select = collection.Get("PlumberList");
                Plumber p = _inventoryService.GetPlumber(select);
                User user = _inventoryService.GetUser(User.Identity.GetUserId());
                int[] counts = new int[2] { 0, 0 };
                if(p!=null && user != null) {
                    string fileName = Path.GetFileName(file.FileName);
                    string Extension = Path.GetExtension(file.FileName);
                    string folderPath = ConfigurationManager.AppSettings["FolderPath"];
                    string FilePath = Server.MapPath(folderPath + fileName);
                    file.SaveAs(FilePath);
                    counts = _inventoryService.ImportToDatabase(p, user, FilePath, Extension, "Yes");

                    System.IO.File.Delete(FilePath);
                }
                TempData["Modified"] = counts[1] + " materials were modified!";
                TempData["Added"] = counts[0] + " new materials were added!";
                return RedirectToAction("Index", "Inventory", null);
            }
            ViewBag.PlumberSelect = new SelectList(_inventoryService.GetPlumberUsers(), "Id", "FullName");
            return RedirectToAction("Index", "Inventory", null);
        }

        public ActionResult LowStock() {
            string UserId = User.Identity.GetUserId();
            Plumber plumber = _inventoryService.GetPlumber(UserId);
            List<Inventory> low = null;
            Supervisor supervisor = _inventoryService.GetSupervisor(UserId);
            if (plumber != null)
                low = _inventoryService.GetLowInventory().Where(x => x.WarehouseId == plumber.WarehouseId).ToList();
            else
                low = _inventoryService.GetLowInventory().ToList();
            ViewBag.Plumber = plumber;
            ViewBag.Supervisor = supervisor;
            return View(low);
        }

        public ActionResult DepletedStock() {
            string UserId = User.Identity.GetUserId();
            Plumber plumber = _inventoryService.GetPlumber(UserId);
            Supervisor supervisor = _inventoryService.GetSupervisor(UserId);
            List<Inventory> depleted = null;
            if (plumber != null)
                depleted = _inventoryService.GetDepletedStock().Where(x => x.WarehouseId == plumber.WarehouseId).ToList();
            else
                depleted = _inventoryService.GetDepletedStock().ToList();
            ViewBag.Plumber = plumber;
            ViewBag.Supervisor = supervisor;
            return View(depleted);
        }

        public ActionResult Edit() {
            return View();
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include ="Id,MaterialId,Quantity,WarehouseId,DateAdded,DateModified,ModifiedBy")] Inventory inventory) {
            if (ModelState.IsValid) {
                _inventoryService.EditInventory(inventory);
                return RedirectToAction("ControlIndex");
            }
            return RedirectToAction("ControlIndex");
        }

        [HttpPost]
        public ActionResult Delete(FormCollection collection) {
            string id = collection.Get("inventoryId");
            int Id = Convert.ToInt32(id);
            _inventoryService.Delete(Id);
            return RedirectToAction("ControlIndex","Inventory",null);
        }
    }
}