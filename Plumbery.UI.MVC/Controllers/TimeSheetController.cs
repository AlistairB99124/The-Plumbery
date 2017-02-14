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
using Plumbery.Infrastructure.Data.Configuration;
using Microsoft.AspNet.Identity.Owin;
using System.Drawing.Printing;
using PdfSharp.Pdf;

namespace Plumbery.UI.MVC.Controllers {
    /// <summary>
    /// COntroller for Time sheet view and functions
    /// </summary>
    [Authorize]
    public class TimeSheetController : Controller {
        private ITimeSheetService _timeSheetService;
        private SignInManager _signInManager;
        private UserManager _userManager;
        #region Properties
        /// <summary>
        /// Sign in manager for logging user
        /// </summary>
        public SignInManager SignInManager {
            get {
                return _signInManager ?? HttpContext.GetOwinContext().Get<SignInManager>();
            }
            private set {
                _signInManager = value;
            }
        }
        /// <summary>
        /// USer Manager to handle User interaction
        /// </summary>
        public UserManager UserManager {
            get {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<UserManager>();
            }
            private set {
                _userManager = value;
            }
        }
        #endregion
        /// <summary>
        /// GET Service dependency in Constructor
        /// </summary>
        /// <param name="timeSheetService"></param>
        public TimeSheetController(ITimeSheetService timeSheetService) {
            this._timeSheetService = timeSheetService;
        }

        public ActionResult PrintPdf(FormCollection collection) {
            string code = collection.Get("Code");
            string path = Server.MapPath("~/Content/documents/timesheets/" + code + ".pdf");
            ProcessStartInfo info = new ProcessStartInfo();            
            info.FileName = path;
            Process p = new Process();
            p.StartInfo = info;
            p.Start();
            return RedirectToAction("Index", "TimeSheet", null);
        }

        [HttpGet]
        public ActionResult Create() {
            // Get collections
            var plumbers = _timeSheetService.GetPlumbers();

            var sites = _timeSheetService.ListSites();
            // Get Plumber logged in
            var plumberSelected = TempData["PlumberSelected"] as Plumber;
            Plumber plumber = null;
            if (plumberSelected == null) {
                plumber = _timeSheetService.GetPlumber(User.Identity.GetUserId());
                if (plumber == null) {
                    plumber = _timeSheetService.GetPlumber(plumbers.FirstOrDefault().UserId);
                }
            } else {
                plumber = plumberSelected;
            }           
            SelectList siteList = new SelectList(sites, "Abbr", "Name");
            List<Material> material = _timeSheetService.ListMaterials(plumber).ToList();
            // Store in view bags
            ViewBag.Materials = material;
            ViewBag.PlumberList = plumbers;
            ViewBag.SiteList = siteList;
            ViewBag.Plumber = plumber;
            //return the view
            return View();
        }

        [HttpGet]
        public JsonResult GetPlumber(int PlumberId) {
            Plumber p = _timeSheetService.GetPlumber(PlumberId);
            TempData["PlumberSelected"] = p;
            return Json(new { Result = 1 }, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]        
        public JsonResult GetTimeSheetId(string code) {
            int id = _timeSheetService.GetTimeSheet(code).Id;
            return Json(new { Result = id },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTimeSheetCount(int PlumberId) {
            int count = _timeSheetService.TimeSheetCount(PlumberId);
            return Json(new { Result = count }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateTimeSheet(TimeSheetModel model) {
            int assistantHours = Convert.ToInt32(model.AssistantHours);
            int assistantMins = Convert.ToInt32(model.AssistantMins);
            int plumberHours = Convert.ToInt32(model.PlumberHours);
            int plumberMins = Convert.ToInt32(model.PlumberMins);
            Site site = _timeSheetService.GetSiteByName(model.SiteName);         
            TimeSpan plumberTime = new TimeSpan(plumberHours, plumberMins, 0);
            TimeSpan assistantTime = new TimeSpan(assistantHours, assistantMins, 0);
            TimeSheet timesheet = new TimeSheet {
                Code = model.Code,
                DateCreated = DateTime.Now,
                Description = model.Description,
                PlumberId = model.PlumberId,
                QuoteNo = model.QuoteNo,
                OriginalQuote = model.OriginalQuote,
                SheetStatus = model.SheetStatus,
                SINumber = model.SINumber,
                SiteId = site.Id,
                SpecificLocation = model.SpecificLocation,
                DetailedPoint = model.DetailedPoint,
                AssistantTime = assistantTime,
                PlumberTime = plumberTime
            };
            _timeSheetService.AddTimeSheet(timesheet);
            TempData["Id"] = timesheet.Id;
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public JsonResult ProcessTable(TableData data) {
            int timesheetId = Convert.ToInt32(TempData["Id"]);
            if (data.BOM_NO == null) {
                TimeSheetCommentItem item = new TimeSheetCommentItem {
                    Metric = data.Quantity,
                    Description = data.Stock_Description,
                    TimeSheetId = timesheetId
                };
                _timeSheetService.AddCommentItem(item);
                TempData["Id"] = timesheetId;
                return Json(new { Result = 1 }, JsonRequestBehavior.DenyGet);
            } else {
                decimal q = 0;
                if (!decimal.TryParse(data.Quantity, out q)) {
                    decimal.TryParse(data.Quantity.Replace(',', '.'), out q);
                }
                int MaterialId = _timeSheetService.GetMaterialByStockCode(data.Stock_Code).Id;
                TimeSheetMaterialItem item = new TimeSheetMaterialItem {
                    MaterialId = MaterialId,
                    BOM_No = data.BOM_NO,
                    Quantity = q,
                    Supplier = data.Supplier,
                    TimeSheetId = timesheetId
                };
                _timeSheetService.AddMaterialItem(item);
                TempData["Id"] = timesheetId;
                return Json(new { Result = 1 }, JsonRequestBehavior.DenyGet);
            }
            
        }
        /// <summary>
        /// Delete entire sheet, including link comments and materials
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteSheet(FormCollection collection) {
            int id = Convert.ToInt32(collection.Get("SheetId"));
            _timeSheetService.DeleteSheet(id);
                return RedirectToAction("Index");
        }
        /// <summary>
        /// Convert Timesheet to PDF and Send to Dropbox or Email
        /// </summary>
        /// <param name="collection">Collection of inputs from the form to perform this function</param>
        /// <returns>Redirect back to Create</returns>
        [HttpPost]
        public JsonResult SendTimeSheet() {
            // Get Timesheet Code from collection
           // string code = Code;
            int timesheetId = Convert.ToInt32(TempData["Id"]);
            // Use code to get timesheet object
            TimeSheet timeSheet = _timeSheetService.GetTimeSheet(timesheetId);
            // Get collections of timesheet items
            List<TimeSheetMaterialItem> materialItems = _timeSheetService.ListMaterialItems(timeSheet.Id).ToList();
            var commentItems = _timeSheetService.ListCommentItems(timeSheet.Id).ToList();
            // Deduct items from inventory
            
            // Populate a datatable to use in the PDF
            DataTable data = new DataTable();
            data.Columns.Add("BOM No.", typeof(string));
            data.Columns.Add("Code", typeof(string));
            data.Columns.Add("Description", typeof(string));
            data.Columns.Add("Quantity", typeof(string));
            data.Columns.Add("Supplier", typeof(string));
            foreach(var m in materialItems) {
                data.Rows.Add(m.BOM_No, m.Material.StockCode, m.Material.StockDescription, m.Quantity.ToString("F2"), m.Supplier);
            }
            commentItems.ToList().ForEach(c => data.Rows.Add("", "", c.Description, c.Metric, ""));
            _timeSheetService.DeductFromInventory(materialItems.ToList());
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
            pdfRenderer.Save(Server.MapPath(filename));
            // ...and start a viewer.
            //Process.Start(Server.MapPath(filename));

            // Drop Box
            //DropboxClient dbx = new DropboxClient("QQm9MqbFkuIAAAAAAAAQqTl01HSny0wZg7sJ4IDy5wRGZIJkfFXXSKgNsZV5pXrs");
            //byte[] content = System.IO.File.ReadAllBytes(Server.MapPath(filename));
            //var result = Upload(dbx, "/Apps/The Plumbery/Daily Time Sheets",timeSheet.Code + ".pdf", content);
            var emailRob = UserManager.SendEmailAttachment("rob@theplumbery.co.za", "Attached is a submitted time sheet.", "Time Sheet: " + timeSheet.Code, Server.MapPath(filename)).Result;
            var emailHeide = UserManager.SendEmailAttachment("heidi@theplumbery.co.za", "Attached is a submitted time sheet.", "Time Sheet: " + timeSheet.Code, Server.MapPath(filename)).Result;
            var emailAdmin = UserManager.SendEmailAttachment("admin@plumbery.org.za", "Attached is a submitted time sheet.", "Time Sheet: " + timeSheet.Code, Server.MapPath(filename)).Result;
            if (emailRob == true && emailHeide == true) {
                return Json(new { Result = 1 }, JsonRequestBehavior.AllowGet);
            }
            //if (emailAdmin == true) {
            //    return Json(new { Result = 1 }, JsonRequestBehavior.AllowGet);
            //}
            return Json(new { Result = 0 }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get list of all time sheets
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            List<TimeSheet> timesheets = null;
            Plumber plumber = _timeSheetService.GetPlumber(User.Identity.GetUserId());
            if (plumber != null) {
                var alltimesheets = _timeSheetService.GetAll();
                timesheets = alltimesheets.ToList().Where(x=>x.PlumberId==plumber.Id).ToList();
            }else {
                var all = _timeSheetService.GetAll();
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
                var updated = dbx.Files.UploadAsync(
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