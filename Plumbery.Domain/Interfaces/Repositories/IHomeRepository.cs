using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    public interface IHomeRepository:IBaseRepository<Home> {
        IEnumerable<Inventory> SearchInventory(List<string> keywords);
        IEnumerable<Warehouse> SearchWarehouse(List<string> keywords);
        IEnumerable<Plumber> SearchPlumbers(List<string> keywords);
        IEnumerable<Supervisor> SearchSupervisors(List<string> keywords);
        IEnumerable<Site> SearchSites(List<string> keywords);
        IEnumerable<TimeSheet> SearchTimeSheets(List<string> keywords); 
    }
}
