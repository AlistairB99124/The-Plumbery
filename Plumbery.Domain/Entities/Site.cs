using System.Collections.Generic;

namespace Plumbery.Domain.Entities {
    public partial class Site {
        public Site() {
            this.TimeSheets = new HashSet<TimeSheet>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
