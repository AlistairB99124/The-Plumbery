using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Plumbery.UI.MVC.Models {
    public class TimeSheetModel {

        [Display(Name = "Specific Location")]
        public string SpecificLocation { get; set; }
        [Required]
        [Display(Name = "Detailed Point")]
        public string DetailedPoint { get; set; }
        [Required]
        [Display(Name = "Completion Status")]
        public Status SheetStatus { get; set; }
        public bool StatusComplete { get; set; }
        [Required]
        public string Description { get; set; }
        [Display(Name = "Time Spent (Plumber)")]
        public string PlumberHours { get; set; }
        public string PlumberMins { get; set; }
        [Display(Name = "Time Spent (Assistant)")]
        public string AssistantHours { get; set; }
        public string AssistantMins { get; set; }
        [Display(Name ="Part of Original Quote?")]
        public bool OriginalQuote { get; set; }
        //public int? QuoteId { get; set; }
        [Display(Name = "Quote No.")]
        public string QuoteNo { get; set; }
        [Display(Name = "S.I. Number")]
        public string SINumber { get; set; }
        [Required]
        [Display(Name = "Plumber")]
        public int PlumberId { get; set; }
        [Required]
        [Display(Name = "Site")]
        public string SiteName { get; set; }
        public string Code { get; set; }
    }

    public class TableData {
        public string BOM_NO { get; set; }
        public string Stock_Code { get; set; }
        public string Stock_Description { get; set; }
        public string Quantity { get; set; }
        public string Supplier { get; set; }
    }

    public class TimeSheetViewModel {
        /*----------Details-------------*/
        public string Code { get; set; }
        [Display(Name = "Specific Location")]
        public string SpecificLocation { get; set; }
        [Display(Name = "Detailed Point")]
        public string DetailedPoint { get; set; }
        [Display(Name = "Completion Status")]
        public Status SheetStatus { get; set; }
        [Display(Name ="Description of Work Done")]
        public string Description { get; set; }
        [Display(Name = "Time Spent (Plumber)")]
        public string PlumberHours { get; set; }
        public string PlumberMins { get; set; }
        [Display(Name = "Time Spent (Assistant)")]
        public string AssistantHours { get; set; }
        public string AssistantMins { get; set; }
        [Display(Name = "Part of Original Quote?")]
        public bool OriginalQuote { get; set; }
        //public int? QuoteId { get; set; }
        [Display(Name = "Quote No.")]
        public string QuoteNo { get; set; }
        [Display(Name = "S.I. Number")]
        public string SINumber { get; set; }
        [Required]
        [Display(Name = "Plumber")]
        public string PlumberId { get; set; }
        [Required]
        [Display(Name = "Site")]
        public int SiteId { get; set; }
        /*----Comments----------*/
        [Display(Name ="Non-Stock Item")]
        public string Comment { get; set; }
        [Display(Name ="Quantity")]
        public string Metric { get; set; }
        /*-------Materials---------*/
        [Display(Name = "BOM No")]
        public int? BOM_No { get; set; }
        [Display(Name = "Material")]
        public int? MaterialId { get; set; }
        public decimal? Quantity { get; set; }
        public string Supplier { get; set; }
    }

    public class ListItemsModel {
        public List<TimeSheetMaterialItem> MaterialItems { get; set; }
        public List<TimeSheetCommentItem> CommentItems { get; set; }
    }
}
