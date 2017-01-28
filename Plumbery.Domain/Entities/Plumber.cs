using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public class Plumber {
        private IUserRepository _repository;
        public int Id { get; set; }
        
        [Display(Name="Warehouse")]
        public int WarehouseId { get; set; }
        [Display(Name ="User")]
        public string UserId { get; set; }

       
        
        public virtual User User { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
