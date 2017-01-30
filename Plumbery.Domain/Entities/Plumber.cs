using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public class Plumber {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Display(Name="Warehouse")]
        public int WarehouseId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Display(Name ="User")]
        public string UserId { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual Warehouse Warehouse { get; set; }
    }
}
