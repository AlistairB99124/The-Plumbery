using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class Warehouse {

        public Warehouse() {
            this.Inventory = new HashSet<Inventory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
