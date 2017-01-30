using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class TimeSheetCommentItem {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Metric { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TimeSheetId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual TimeSheet TimeSheet { get; set; }
    }
}
