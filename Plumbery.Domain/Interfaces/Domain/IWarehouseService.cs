using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface IWarehouseService {
        Task<int> Add(Warehouse warehouse);
        Task<int> Edit(Warehouse warehouse);
        Task<int> Delete(Warehouse warehouse);
        void List();
        Warehouse Get(int? id);
        IEnumerable<Warehouse> GetAll();
    }
}
