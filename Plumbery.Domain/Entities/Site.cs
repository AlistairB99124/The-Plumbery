using System.Collections.Generic;

namespace Plumbery.Domain.Entities {
    public partial class Site {
        /// <summary>
        /// 
        /// </summary>
        public Site() {
            this.TimeSheets = new HashSet<TimeSheet>();
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
        public string Abbr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual Location Location { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}
