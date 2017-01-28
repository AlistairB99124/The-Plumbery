using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class PlumberRepository : BaseRepository<Plumber>, IPlumberRepository {
        public Supervisor GetSupervisor(string UserId) => _context.Supervisors.Where(x => x.UserId == UserId).FirstOrDefault();

        public IEnumerable<User> GetUsers() => _context.Users;

        public IEnumerable<Warehouse> GetWarehouses() => _context.Warehouses;
    }
}
