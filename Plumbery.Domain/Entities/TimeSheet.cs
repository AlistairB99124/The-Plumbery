using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class TimeSheet {

        public TimeSheet() {
            this.Materials = new HashSet<TimeSheetMaterialItem>();
            this.Comments = new HashSet<TimeSheetCommentItem>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public string SpecificLocation { get; set; }
        public string DetailedPoint { get; set; }       
        public Status SheetStatus { get; set; }
        public bool StatusComplete { get; set; }
        public string Description { get; set; }
        public TimeSpan PlumberTime { get; set; }
        public TimeSpan AssistantTime { get; set; }
        public bool OriginalQuote { get; set; }
        //public int? QuoteId { get; set; }
        public string QuoteNo { get; set; }
        public string SINumber { get; set; }
        public int PlumberId { get; set; }
        public int SiteId { get; set; }

        public virtual Plumber Plumber { get; set; }
        public virtual Site Site { get; set; }
        //public virtual Quote Quote { get; set; }
        public virtual ICollection<TimeSheetMaterialItem> Materials { get; set; }
        public virtual ICollection<TimeSheetCommentItem> Comments { get; set; }
    }
    public enum Status {
        Chasing = 0,
        FirstFix = 1,
        SecondFix = 2,
        FinalFix = 3
    }
}
