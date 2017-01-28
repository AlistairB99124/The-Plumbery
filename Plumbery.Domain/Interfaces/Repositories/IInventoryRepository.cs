using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Plumbery.Domain.Interfaces.Repositories {
    public interface IInventoryRepository : IBaseRepository<Inventory> {
        Inventory GetInventoryByCode(string code);
        User GetUser(string Id);
        void AddMaterial(Material material);
        Plumber GetPlumber(string UserId);
        Plumber GetPlumber(int Id);
        int[] ImportToDatabase(Plumber p, User user, string filePath, string extension, string firstRowHeader);
        IEnumerable<User> GetPlumberUsers();

        IEnumerable<Inventory> GetLowInventory();
        IEnumerable<Inventory> GetDepletedStock();
        IEnumerable<Inventory> GetInventoryByWarehouse(int warehouseId);
        Supervisor GetSupervisor(string UserId);

    }
}
