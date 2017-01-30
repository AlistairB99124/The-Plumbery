using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class Material {
        /// <summary>
        /// 
        /// </summary>
        public Material() {
            this.Items = new HashSet<TimeSheetMaterialItem>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StockCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StockDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TimeSheetMaterialItem> Items { get; set; }
    }
}
