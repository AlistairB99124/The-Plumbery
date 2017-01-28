using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Entities {
    public partial class Supervisor {
        public int Id { get; set; }
        [Display(Name="User")]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
