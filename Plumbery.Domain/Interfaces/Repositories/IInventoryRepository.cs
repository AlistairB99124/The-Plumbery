using Plumbery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Plumbery.Domain.Interfaces.Repositories {
    /// <summary>
    /// 
    /// </summary>
    public interface IInventoryRepository : IBaseRepository<Inventory> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Inventory GetInventoryByCode(string code);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        User GetUser(string Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="material"></param>
        void AddMaterial(Material material);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Plumber GetPlumber(string UserId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Plumber GetPlumber(int Id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="user"></param>
        /// <param name="filePath"></param>
        /// <param name="extension"></param>
        /// <param name="firstRowHeader"></param>
        /// <returns></returns>
        int[] ImportToDatabase(Plumber p, User user, string filePath, string extension, string firstRowHeader);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetPlumberUsers();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Inventory> GetLowInventory();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Inventory> GetDepletedStock();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        IEnumerable<Inventory> GetInventoryByWarehouse(int warehouseId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Supervisor GetSupervisor(string UserId);

    }
}
