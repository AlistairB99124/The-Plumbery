using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Domain {
    public interface IInventoryService {
        Inventory GetInventoryByCode(string code);
        void EditInventory(Inventory inventory);
        void AddInventory(Inventory inventory);
        User GetUser(string Id);
        void AddMaterial(Material material);
        Plumber GetPlumber(int Id);
        Plumber GetPlumber(string UserId);
        int[] ImportToDatabase(Plumber p, User user, string filePath, string extension, string firstRowHeader);
        IEnumerable<User> GetPlumberUsers();
        IEnumerable<Inventory> GetLowInventory();
        IEnumerable<Inventory> GetDepletedStock();
        IEnumerable<Inventory> GetInventoryByWarehouse(int warehouseId);
        IEnumerable<Inventory> ListAll();
        Supervisor GetSupervisor(string UserId);
        List<Inventory> GetAll();
        void Delete(int id);
        void Edit(Inventory inventory);
    }
}
