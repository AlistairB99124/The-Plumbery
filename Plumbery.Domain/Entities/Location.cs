using System.Collections.Generic;

namespace Plumbery.Domain.Entities {
    public partial class Location {
        public Location() {
            this.Sites = new HashSet<Site>();
        }
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string Postal_Code { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}
