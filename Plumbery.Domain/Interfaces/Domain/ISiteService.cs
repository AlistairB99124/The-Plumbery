using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface ISiteService {
        Task<int> Add(Site site, Location location);
        Task<int> Edit(Site site);
        Task<int> Delete(Site site);
        void List();
        Task<int> AddLocation(Location location);
        Site Get(int? id);
        Location GetLocation(int locationId);
        Task<int> EditLocation(Location location);
        IEnumerable<Site> GetAll();
    }
}
