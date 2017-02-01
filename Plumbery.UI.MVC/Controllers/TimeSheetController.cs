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
using Dropbox.Api;
using System.Threading.Tasks;
using System.IO;
using Dropbox.Api.Files;
using System.Text;

namespace Plumbery.UI.MVC.Controllers {
    /// <summary>
    /// COntroller for Time sheet view and functions
    /// </summary>
    [Authorize]
    public class TimeSheetController : Controller {
        private ITimeSheetService _timeSheetService;
        /// <summary>
        /// GET Service dependency in Constructor
        /// </summary>
        /// <param name="timeSheetService"></param>
        public TimeSheetController(ITimeSheetService timeSheetService) {
            this._timeSheetService = timeSheetService;
        }

        /// <summary>
        /// GET view for Creating a SHeet
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Create() {
            // Get collections
            var plumbers = await _timeSheetService.GetPlumberUsers();
            var sites = await _timeSheetService.ListSites();
            // Get Plumber logged in
            var plumber = await _timeSheetService.GetPlumber(User.Identity.GetUserId());
            // Specify SlectLists for dropdown boxes
            SelectList plumberList = null;
            if (plumber != null) {
                plumberList = new SelectList(plumbers, "Id", "FullName", plumber.UserId);
            } else {
                plumberList = new SelectList(plumbers, "Id", "FullName");
            }
            SelectList siteList = new SelectList(sites, "Id", "Name");
            // Store in view bags
            ViewBag.PlumberList = plumberList;
            ViewBag.SiteList = siteList;
            //return the view
            return View();
        }
        /// <summary>
        /// Create the timesheet from the input form
        /// </summary>
        /// <param name="model">Model representing Time Sheet Data</param>
        /// <param name="collection">COllection to get data from select inputs</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TimeSheetModel model, FormCollection collection) {
            try {
                var site = await _timeSheetService.GetSite(model.SiteId);
                string id = model.PlumberId;
                //Plumber plumber = _timeSheetService.GetPlumber(model.PlumberId);
                Plumber plumber = await _timeSheetService.GetPlumber(model.PlumberId);
                var timesheets = await _timeSheetService.GetAll();

                var timesheetToday = timesheets.Where(x => x.PlumberId == plumber.Id).Where(x => x.DateCreated.Date == DateTime.Now.Date).ToList();
                string code = site.Abbr + "-" + plumber.Id + "-" + timesheetToday.Count+1 + DateTime.Now.ToString("dMyy");
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
                var result = await _timeSheetService.AddTimeSheet(sheet);
                if (result > 0) {
                    return RedirectToAction("Items", new { code = sheet.Code });
                }
                return View(model);
            } catch {
                // Get collections
                var plumbers = await _timeSheetService.GetPlumberUsers();
                var sites = await _timeSheetService.ListSites();
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
        /// <summary>
        /// Get form to add items to a given timesheet        /// 
        /// </summary>
        /// <param name="code">Time sheet code</param>
        /// <returns></returns>
        public async Task<ActionResult> Items(string code) {
            try {
                var sheet = await _timeSheetService.GetTimeSheet(code);
                var plumber = await _timeSheetService.GetPlumber(sheet.PlumberId);
                var materialItems = await _timeSheetService.ListMaterialItems(sheet.Id);
                var commentItems = await _timeSheetService.ListCommentItems(sheet.Id);
                var materials = await _timeSheetService.ListMaterials(plumber);

                SelectList materialList = new SelectList(materials.ToList(), "Id", "SelectDescription", materials.FirstOrDefault());
                
                ViewBag.Error = TempData["Failure"];
                ViewBag.Success = TempData["Success"];
                ViewBag.MaterialList = materialList;
                ViewBag.Materials = materialItems.ToList();
                ViewBag.Comments = commentItems.ToList();
                ViewBag.TimeSheetCode = sheet.Code;
                return View();
            } catch (Exception x) {
                return View();
            }           
        }
        /// <summary>
        /// Add material to time sheet
        /// </summary>
        /// <param name="model">Model representing Material Data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddMaterial(MaterialItemModel model) {
            try {
                var timeSheet = await _timeSheetService.GetTimeSheet(model.TimeSheetCode);
                TimeSheetMaterialItem item = new TimeSheetMaterialItem {
                    BOM_No = model.BOM_No,
                    Quantity = Convert.ToDecimal(model.Quantity),
                    MaterialId = Convert.ToInt32(model.MaterialId),
                    TimeSheetId = timeSheet.Id,
                    Supplier = model.Supplier
                };
                var result = await _timeSheetService.AddMaterialItem(item);

                Plumber plumber = await _timeSheetService.GetPlumber(timeSheet.PlumberId);
                IEnumerable<Material> materials = await _timeSheetService.ListMaterials(plumber);
                SelectList materialList = new SelectList(materials, "Id", "Description", materials.FirstOrDefault());
                ViewBag.AvailableMaterials = materials;
                TempData["Success"] = "A material was succeessfully added!";
                return RedirectToAction("Items", new { code = model.TimeSheetCode });
            } catch {
                TempData["Failure"] = "Item was not added!";
                var timeSheet = await _timeSheetService.GetTimeSheet(model.TimeSheetCode);
                Plumber plumber = await _timeSheetService.GetPlumber(timeSheet.PlumberId);
                IEnumerable<Material> materials = await _timeSheetService.ListMaterials(plumber);
                SelectList materialList = new SelectList(materials, "Id", "Description", materials.FirstOrDefault());
                ViewBag.AvailableMaterials = materials;
                return RedirectToAction("Items", new { code = model.TimeSheetCode });
            }

        }
        /// <summary>
        /// Delete Item from Time Sheet
        /// </summary>
        /// <param name="collection">Collection of inputs from Form</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteMaterialItem(FormCollection collection) {
            int MaterialId = Convert.ToInt32(collection.Get("MaterialId"));
            string timesheetCode = collection.Get("timesheetCode");
            var result = await _timeSheetService.DeleteMaterialItem(MaterialId);
            if (result > 0) {
                return RedirectToAction("Items", "TimeSheet", new { code = timesheetCode });
            }
            return RedirectToAction("Items", "TimeSheet", new { code = timesheetCode });
        }
        /// <summary>
        /// POST delete comment from time sheet
        /// </summary>
        /// <param name="collection">collection to get ids</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteCommentItem(FormCollection collection) {
            int commentId = Convert.ToInt32(collection.Get("CommentId"));
            string timesheetCode = collection.Get("timesheetCode");
            var result = await _timeSheetService.DeleteCommentItem(commentId);
            if (result > 0) {
                return RedirectToAction("Items", "TimeSheet", new { code = timesheetCode });
            }
            return RedirectToAction("Items", "TimeSheet", new { code = timesheetCode });
        }
        /// <summary>
        /// POST add comment to time sheet
        /// </summary>
        /// <param name="model">Model representing comment data</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddComment(CommentItemModel model) {
            try {
                var timeSheet = await _timeSheetService.GetTimeSheet(model.TimeSheetCode);
                TimeSheetCommentItem item = new TimeSheetCommentItem {
                    Description = model.Description,
                    Metric = model.Metric,
                    TimeSheetId = timeSheet.Id
                };
                var result = await _timeSheetService.AddCommentItem(item);
                if (result > 0) {
                    TempData["Success"] = "A comment was successfully added!";
                    return RedirectToAction("Items", new { code = model.TimeSheetCode });
                }          
                return RedirectToAction("Items", new { code = model.TimeSheetCode });
            } catch {
                TempData["Error"] = "Item was not added!";                
                return RedirectToAction("Items", new { code = model.TimeSheetCode });
            }
        }
        /// <summary>
        /// Delete entire sheet, including link comments and materials
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> DeleteSheet(FormCollection collection) {
            int id = Convert.ToInt32(collection.Get("SheetId"));
            var result = await _timeSheetService.DeleteSheet(id);
            if (result > 0) {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Clear all items from the Time Sheet
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ClearAll(FormCollection collection) {
            string code = collection.Get("TimeSheetCode");
            TimeSheet sheet = await _timeSheetService.GetTimeSheet(code);
            IEnumerable<TimeSheetMaterialItem> materialItems = await _timeSheetService.ListMaterialItems(sheet.Id);
            IEnumerable<TimeSheetCommentItem> commentItems = await _timeSheetService.ListCommentItems(sheet.Id);
            var result = await _timeSheetService.DeleteCommentItems(commentItems.ToList());
            var result2 = await _timeSheetService.DeleteMaterialItems(materialItems.ToList());

            return RedirectToAction("Items", "TimeSheet", new { TimeSheet = code });
        }
        /// <summary>
        /// Convert Timesheet to PDF and Send to Dropbox or Email
        /// </summary>
        /// <param name="collection">Collection of inputs from the form to perform this function</param>
        /// <returns>Redirect back to Create</returns>
        [HttpPost]
        public async Task<ActionResult> SendTimeSheet(FormCollection collection) {
            // Get Timesheet Code from collection
            string code = collection.Get("timesheetCode");
            // Use code to get timesheet object
            TimeSheet timeSheet = await _timeSheetService.GetTimeSheet(code);
            // Get collections of timesheet items
            var materialItems = await _timeSheetService.ListMaterialItems(timeSheet.Id);
            var commentItems = await _timeSheetService.ListCommentItems(timeSheet.Id);
            // Deduct items from inventory
            var result = await _timeSheetService.DeductFromInventory(materialItems.ToList());
            // Populate a datatable to use in the PDF
            DataTable data = new DataTable();
            data.Columns.Add("BOM No.", typeof(string));
            data.Columns.Add("Code", typeof(string));
            data.Columns.Add("Description", typeof(string));
            data.Columns.Add("Quantity", typeof(string));
            data.Columns.Add("Supplier", typeof(string));
            materialItems.ToList().ForEach(m => data.Rows.Add(m.BOM_No, m.Material.StockCode, m.Material.StockDescription, m.Quantity.ToString("F2"), m.Supplier));
            commentItems.ToList().ForEach(c => data.Rows.Add("", "", c.Description, c.Metric, ""));

            // Assign timesheet code to document name
            data.TableName = timeSheet.Code;
            // Start PDF generation
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
            //Process.Start(Server.MapPath(filename));

            // Drop Box
            DropboxClient dbx = new DropboxClient("QQm9MqbFkuIAAAAAAAAQqTl01HSny0wZg7sJ4IDy5wRGZIJkfFXXSKgNsZV5pXrs");
            byte[] content = System.IO.File.ReadAllBytes(Server.MapPath(filename));
            await Upload(dbx, "/Apps/The Plumbery/Daily Time Sheets",timeSheet.Code + ".pdf", content);

            return RedirectToAction("Create", "TimeSheet", null);
        }
        /// <summary>
        /// Get list of all time sheets
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index() {
            List<TimeSheet> timesheets = null;
            Plumber plumber = await _timeSheetService.GetPlumber(User.Identity.GetUserId());
            if (plumber != null) {
                var alltimesheets = await _timeSheetService.GetAll();
                timesheets = alltimesheets.ToList().Where(x=>x.PlumberId==plumber.Id).ToList();
            }else {
                var all = await _timeSheetService.GetAll();
                timesheets = all.ToList();
            }

            return View(timesheets);
        }
        /// <summary>
        /// Upload pdf to Dropbox
        /// </summary>
        /// <param name="dbx">Drop Box Client</param>
        /// <param name="folder">Folder in Drop Box</param>
        /// <param name="file">File Name to be saved in folder</param>
        /// <param name="content">Content eg pdf byte[] from memory stream</param>
        /// <returns></returns>
        public async Task<string> Upload(DropboxClient dbx, string folder, string file, byte[] content) {
            using (MemoryStream ms = new MemoryStream(content)) {
                var updated = await dbx.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: ms);
                var arg1 = new Dropbox.Api.Sharing.CreateSharedLinkWithSettingsArg(folder + "/" + file);
                var share = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(arg1);
                return share.Url;
            }
        }
    }
}