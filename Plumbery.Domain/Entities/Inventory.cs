using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class Inventory {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WarehouseId { get; set; }
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
        public DateTime DateAdded { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DateModified { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Material Material { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }
    }
}
