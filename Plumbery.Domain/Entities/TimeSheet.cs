using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class TimeSheet {
        /// <summary>
        /// 
        /// </summary>
        public TimeSheet() {
            this.Materials = new HashSet<TimeSheetMaterialItem>();
            this.Comments = new HashSet<TimeSheetCommentItem>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SpecificLocation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DetailedPoint { get; set; }    
           
        public Status SheetStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool StatusComplete { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan PlumberTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan AssistantTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool OriginalQuote { get; set; }
        //public int? QuoteId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string QuoteNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SINumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PlumberId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int SiteId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Plumber Plumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Site Site { get; set; }
        //public virtual Quote Quote { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TimeSheetMaterialItem> Materials { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TimeSheetCommentItem> Comments { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public enum Status {
        Chasing = 0,
        FirstFix = 1,
        SecondFix = 2,
        FinalFix = 3
    }
}
