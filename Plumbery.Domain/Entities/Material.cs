using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class Material {
        public Material() {
            this.Items = new HashSet<TimeSheetMaterialItem>();
        }
        public int Id { get; set; }
        public string StockCode { get; set; }
        public string StockDescription { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<TimeSheetMaterialItem> Items { get; set; }
    }
}
