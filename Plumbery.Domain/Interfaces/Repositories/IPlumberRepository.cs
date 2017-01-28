using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    public interface IPlumberRepository:IBaseRepository<Plumber> {
        IEnumerable<User> GetUsers();
        IEnumerable<Warehouse> GetWarehouses();
        Supervisor GetSupervisor(string UserId);
    }
}
