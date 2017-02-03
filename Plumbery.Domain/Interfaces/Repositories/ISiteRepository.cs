using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    public interface ISiteRepository:IBaseRepository<Site> {
        void AddLocation(Location location);
        void EditLocation(Location location);
        Location GetLocation(int locationId);
        void AddSite(Site site, Location location);
    }
}
