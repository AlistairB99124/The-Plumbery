using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Interfaces.Repositories;
using Plumbery.Domain.Entities;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class SiteRepository:BaseRepository<Site>, ISiteRepository {
        public void AddLocation(Location location) => _context.Locations.Add(location);

        public void EditLocation(Location location) => _context.Entry(location).State = System.Data.Entity.EntityState.Modified;

        public Location GetLocation(int locationId) => _context.Locations.Find(locationId);
    }
}
