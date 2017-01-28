using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Plumbery.UI.MVC.Models;
using System.Data;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel;
using Plumbery.UI.MVC.Utilities;
using System.Diagnostics;

namespace Plumbery.UI.MVC.Controllers {
    [Authorize]
    public class TimeSheetController : Controller {
        private ITimeSheetService _timeSheetService;

        public TimeSheetController(ITimeSheetService timeSheetService) {
            this._timeSheetService = timeSheetService;
        }

        // GET: TimeSheet
        public ActionResult Create() {
            // Get collections
            var plumbers = _timeSheetService.GetPlumberUsers();
            var sites = _timeSheetService.ListSites();
            // Get Plumber logged in
            var plumber = _timeSheetService.GetPlumber(User.Identity.GetUserId());
            // Specify SlectLists for dropdown boxes
            SelectList plumberList = new SelectList(plumbers, "Id", "FullName", plumber.UserId);
            SelectList siteList = new SelectList(sites, "Id", "Name");
            // Store in view bags
            ViewBag.PlumberList = plumberList;
            ViewBag.SiteList = siteList;
            //return the view
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TimeSheetModel model, FormCollection collection) {
            try {
                Site site = _timeSheetService.GetSite(model.SiteId);
                string id = model.PlumberId;
                Plumber plumber = _timeSheetService.GetPlumber(model.PlumberId);
                string code = site.Abbr + "-" + plumber.Id + "-" + DateTime.Now.ToString("ddMMyyhhmmss");
                string ph = collection.Get("PlumberHours");
                string pm = collection.Get("PlumberMins");
                string ah = collection.Get("AssistantHours");
                string am = collection.Get("AssistantMins");
                int PlumberHours = Convert.ToInt32(collection.Get("PlumberHours"));
                int PlumberMins = Convert.ToInt32(collection.Get("PlumberMins"));
                int AssistantHours = Convert.ToInt32(collection.Get("AssistantHours"));
                int AssistantMins = Convert.ToInt32(collection.Get("AssistantMins"));
                TimeSpan PlumberTime = new TimeSpan(PlumberHours, PlumberMins, 0);
                TimeSpan AssistantTime = new TimeSpan(AssistantHours, AssistantMins, 0);
                int statusId = Convert.ToInt32(collection.Get("statusSlider"));
                Status status = (Status)statusId;
                TimeSheet sheet = new TimeSheet {
                    Code = code,
                    PlumberId = plumber.Id,
                    SpecificLocation = model.SpecificLocation,
                    DetailedPoint = model.DetailedPoint,
                    SheetStatus = status,
                    Description = model.Description,
                    PlumberTime = PlumberTime,
                    AssistantTime = AssistantTime,
                    OriginalQuote = model.OriginalQuote,
                    QuoteNo = model.QuoteNo,
                    SiteId = model.SiteId,
                    SINumber = model.SINumber,
                    DateCreated = DateTime.Now
                };
                _timeSheetService.AddTimeSheet(sheet);

                return RedirectToAction("Items", new { TimeSheet = code });
            } catch {
                // Get collections
                List<User> plumbers = _timeSheetService.GetPlumberUsers().ToList();
                var sites = _timeSheetService.ListSites();
                //// Get Plumber logged in
                var currentPlumber = _timeSheetService.GetPlumber(User.Identity.GetUserId());
                //Specify SlectLists for dropdown boxes
                SelectList plumberList = new SelectList(plumbers, "Id", "FullName", currentPlumber.Id, null);
                SelectList siteList = new SelectList(sites, "Id", "Name");
                // Store in view bags
                ViewBag.PlumberList = plumberList;
                ViewBag.SiteList = siteList;
                ViewBag.Plumber = currentPlumber;
                //return the view
                return View();
            }
        }

        public ActionResult Items(string TimeSheet) {
            TimeSheet sheet = _timeSheetService.GetTimeSheet(TimeSheet);
            Plumber plumber = _timeSheetService.GetPlumber(sheet.PlumberId);
            IEnumerable<TimeSheetMaterialItem> materialItems = _timeSheetService.ListMaterialItems(sheet.Id);
            IEnumerable<TimeSheetCommentItem> commentItems = _timeSheetService.ListCommentItems(sheet.Id);
            IEnumerable<Material> materials = _timeSheetService.ListMaterials(plumber);
            SelectList materialList = new SelectList(materials, "Id", "StockDescription", materials.FirstOrDefault());

            ViewBag.Error = ViewData["Failure"];
            ViewBag.Success = TempData["Success"];
            ViewBag.MaterialList = materialList;
            ViewBag.Materials = materialItems;
            ViewBag.Comments = commentItems;
            ViewBag.TimeSheetCode = sheet.Code;
            return View();
        }

        [HttpPost]
        public ActionResult AddMaterial(CommentAndMaterialModel model) {
            try {
                var timeSheet = _timeSheetService.GetTimeSheet(model.TimeSheetCode);
                TimeSheetMaterialItem item = new TimeSheetMaterialItem {
                    BOM_No = model.BOM_No,
                    Quantity = model.Quantity,
                    MaterialId = model.MaterialId,
                    TimeSheetId = timeSheet.Id,
                    Supplier = model.Supplier
                };
                _timeSheetService.AddMaterialItem(item);
                Plumber plumber = _timeSheetService.GetPlumber(timeSheet.PlumberId);
                IEnumerable<Material> materials = _timeSheetService.ListMaterials(plumber);
                SelectList materialList = new SelectList(materials, "Id", "Description", materials.FirstOrDefault());
                ViewBag.AvailableMaterials = materials;
                TempData["Success"] = "A material was succeessfully added!";
                return RedirectToAction("Items", new { TimeSheet = model.TimeSheetCode });
            } catch {
                ViewData["Failure"] = "Item was not added!";
                var timeSheet = _timeSheetService.GetTimeSheet(model.TimeSheetCode);
                Plumber plumber = _timeSheetService.GetPlumber(timeSheet.PlumberId);
                IEnumerable<Material> materials = _timeSheetService.ListMaterials(plumber);
                SelectList materialList = new SelectList(materials, "Id", "Description", materials.FirstOrDefault());
                ViewBag.AvailableMaterials = materials;
                return RedirectToAction("Items", new { TimeSheet = model.TimeSheetCode });
            }

        }

        [HttpPost]
        public ActionResult DeleteMaterialItem(FormCollection collection) {
            int MaterialId = Convert.ToInt32(collection.Get("MaterialId"));
            string timesheetCode = collection.Get("timesheetCode");
            _timeSheetService.DeleteMaterialItem(MaterialId);
            return RedirectToAction("Items", "TimeSheet", new { TimeSheet = timesheetCode });
        }

        [HttpPost]
        public ActionResult DeleteCommentItem(FormCollection collection) {
            int commentId = Convert.ToInt32(collection.Get("CommentId"));
            string timesheetCode = collection.Get("timesheetCode");
            _timeSheetService.DeleteCommentItem(commentId);
            return RedirectToAction("Items", "TimeSheet", new { TimeSheet = timesheetCode });
        }

        public ActionResult AddComment() {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(CommentAndMaterialModel model) {
            try {
                var timeSheet = _timeSheetService.GetTimeSheet(model.TimeSheetCode);
                TimeSheetCommentItem item = new TimeSheetCommentItem {
                    Description = model.Description,
                    Metric = model.Metric,
                    TimeSheetId = timeSheet.Id
                };
                _timeSheetService.AddCommentItem(item);

                TempData["Success"] = "A comment was successfully added!";
                return RedirectToAction("Items", new { TimeSheet = model.TimeSheetCode });
            } catch {
                TempData["Error"] = "Item was not added!";
                return RedirectToAction("Items", new { TimeSheet = model.TimeSheetCode });
            }
        }

        [HttpPost]
        public ActionResult ClearAll(FormCollection collection) {
            string code = collection.Get("TimeSheetCode");
            TimeSheet sheet = _timeSheetService.GetTimeSheet(code);
            List<TimeSheetMaterialItem> materialItems = _timeSheetService.ListMaterialItems(sheet.Id).ToList();
            List<TimeSheetCommentItem> commentItems = _timeSheetService.ListCommentItems(sheet.Id).ToList();
            _timeSheetService.DeleteCommentItems(commentItems);
            _timeSheetService.DeleteMaterialItems(materialItems);

            return RedirectToAction("Items", "TimeSheet", new { TimeSheet = code });
        }

        [HttpPost]
        public ActionResult SendTimeSheet(FormCollection collection) {
            string code = collection.Get("timesheetCode");
            TimeSheet timeSheet = _timeSheetService.GetTimeSheet(code);
            var materialItems = _timeSheetService.ListMaterialItems(timeSheet.Id).ToList();
            var commentItems = _timeSheetService.ListCommentItems(timeSheet.Id).ToList();
            _timeSheetService.DeductFromInventory(materialItems);
            DataTable data = new DataTable();
            data.Columns.Add("BOM No.", typeof(string));
            data.Columns.Add("Code", typeof(string));
            data.Columns.Add("Description", typeof(string));
            data.Columns.Add("Quantity", typeof(string));
            data.Columns.Add("Supplier", typeof(string));
            materialItems.ForEach(m => data.Rows.Add(m.BOM_No, m.Material.StockCode, m.Material.StockDescription, m.Quantity.ToString("F2"), m.Supplier));
            commentItems.ForEach(c => data.Rows.Add("", "", c.Description, c.Metric, ""));

            data.TableName = timeSheet.Code;

            PdfHelper pdfHelper = new PdfHelper(timeSheet, data);
            // Create a MigraDoc document
            Document document = pdfHelper.CreateDocument();
            document.UseCmykColor = true;
            // Create a renderer for PDF that uses Unicode font encoding
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);

            // Set the MigraDoc document
            pdfRenderer.Document = document;


            // Create the PDF document
            pdfRenderer.RenderDocument();

            // Save the PDF document...
            string filename = "~/Content/documents/timesheets/" + timeSheet.Code + ".pdf";
#if DEBUG
            // I don't want to close the document constantly...
            filename = "~/Content/documents/timesheets/" + timeSheet.Code + ".pdf";
#endif
            pdfRenderer.Save(Server.MapPath(filename));
            // ...and start a viewer.
            Process.Start(Server.MapPath(filename));
            return RedirectToAction("Create", "TimeSheet", null);
        }

        public ActionResult Index() {
            List<TimeSheet> timesheets = null;
            Plumber plumber = _timeSheetService.GetPlumber(User.Identity.GetUserId());
            if (plumber != null) {
                timesheets = _timeSheetService.GetAll().ToList().Where(x=>x.PlumberId==plumber.Id).ToList();
            }else {
                timesheets = _timeSheetService.GetAll().ToList();
            }

            return View(timesheets);
        }
    }
}