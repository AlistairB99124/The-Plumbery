using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class TimeSheetMaterialItem {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string BOM_No { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaterialId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TimeSheetId { get; set;}
        /// <summary>
        /// 
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSheet TimeSheet { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Material Material { get; set; }
        
    }
}
