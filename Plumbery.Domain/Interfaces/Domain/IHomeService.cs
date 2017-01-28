using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface IHomeService {
        IEnumerable<Inventory> SearchInventory(List<string> keywords);
        IEnumerable<Warehouse> SearchWarehouse(List<string> keywords);
        IEnumerable<Plumber> SearchPlumbers(List<string> keywords);
        IEnumerable<Supervisor> SearchSupervisors(List<string> keywords);
        IEnumerable<Site> SearchSites(List<string> keywords);
        IEnumerable<TimeSheet> SearchTimeSheets(List<string> keywords);
    }
}
