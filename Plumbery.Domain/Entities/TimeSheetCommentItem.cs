using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class TimeSheetCommentItem {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Metric { get; set; }
        public int TimeSheetId { get; set; }
        

        public virtual TimeSheet TimeSheet { get; set; }
    }
}
