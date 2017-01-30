using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class Warehouse {
        /// <summary>
        /// 
        /// </summary>
        public Warehouse() {
            this.Inventory = new HashSet<Inventory>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
