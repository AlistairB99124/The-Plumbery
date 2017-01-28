using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class TimeSheetMaterialItem {
        public int Id { get; set; }
        public int? BOM_No { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set; }
        public int TimeSheetId { get; set;}
        public string Supplier { get; set; }
        public string StockCode { get { return GetMaterialNumber(); } }

        public TimeSheet TimeSheet { get; set; }
        public virtual Material Material { get; set; }

        public TimeSheetMaterialItem() {

        }

        private string GetMaterialNumber() {
            return Material.StockCode;
        }
    }
}
