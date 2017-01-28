using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.UI.MVC.Models {
    public class CreatePlumberViewModel {
        public string UserId { get; set; }
        public int WarehouseId { get; set; }
    }
    public class EditPlumberViewModel {
        public int Id { get; set;}
        public string UserId { get; set; }
        public int WarehouseId { get; set; }
    }
}
