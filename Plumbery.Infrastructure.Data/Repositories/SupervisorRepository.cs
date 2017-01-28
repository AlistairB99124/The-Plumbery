using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Infrastructure.Data.Repositories {
    public class SupervisorRepository : BaseRepository<Supervisor>, ISupervisorRepository {
        public Supervisor Get(string v) => _context.Supervisors.Where(x => x.UserId == v).FirstOrDefault();

        public IEnumerable<User> GetUsers() => _context.Users;
    }
}
