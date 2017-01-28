using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    public interface ISupervisorRepository:IBaseRepository<Supervisor> {
        IEnumerable<User> GetUsers();
        Supervisor Get(string v);
    }
}
