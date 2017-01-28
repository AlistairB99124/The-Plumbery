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
        public string PlumberId { get; set; }
        [Required]
        [Display(Name = "Site")]
        public int SiteId { get; set; }
    }

    public class CommentAndMaterialModel {
        public string TimeSheetCode { get; set; }
        public string Description { get; set; }
        public string Metric { get; set; }
        [Display(Name ="BOM No")]
        public int? BOM_No { get; set; }
        [Display(Name ="Material")]
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public string Supplier { get; set; }
    }
}
