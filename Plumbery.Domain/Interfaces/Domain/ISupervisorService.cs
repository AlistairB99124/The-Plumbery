using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface ISupervisorService {
        IEnumerable<User> GetUsers();
        void AddSupervisor(Supervisor Supervisor);
        Supervisor GetSupervisor(int? id);
        void EditSupervisor(Supervisor Supervisor);
        void DeleteSupervisor(int id);
        Supervisor GetSupervisor(string v);
        IEnumerable<Supervisor> GetAll();
    }
}
