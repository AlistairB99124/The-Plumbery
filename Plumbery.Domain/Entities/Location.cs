using System.Collections.Generic;

namespace Plumbery.Domain.Entities {
    /// <summary>
    /// 
    /// </summary>
    public partial class Location {
        /// <summary>
        /// 
        /// </summary>
        public Location() {
            this.Sites = new HashSet<Site>();
        }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Address2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Postal_Code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Site> Sites { get; set; }
    }
}
