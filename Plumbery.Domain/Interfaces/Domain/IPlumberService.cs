using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface IPlumberService {
        IEnumerable<User> GetUsers();
        void AddPlumber(Plumber plumber);
        Plumber GetPlumber(int? id);
        void EditPlumber(Plumber plumber);
        void DeletePlumber(int id);
        IEnumerable<Warehouse> GetWarehouses();
        IEnumerable<Plumber> GetPlumbers();
        Supervisor GetSupervisor(string UserId);
    }
}
