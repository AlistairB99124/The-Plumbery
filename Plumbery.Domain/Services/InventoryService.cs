using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Services {
    public class InventoryService : ServiceBase, IInventoryService {
        #region Private Field

        private IInventoryRepository _inventoryRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise TimeSheet Repository
        /// </summary>
        /// <param name="TimeSheetRepository"></param>
        public InventoryService(IInventoryRepository inventoryRepository) {
            _inventoryRepository = inventoryRepository;
        }

        public void AddInventory(Inventory inventory) {
            try {
                StartTransaction();
                _inventoryRepository.Add(inventory);
                PersistTransaction();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void AddMaterial(Material material) {
            try {
                StartTransaction();
                _inventoryRepository.AddMaterial(material);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void Delete(int id) {
            try {
                StartTransaction();
                _inventoryRepository.Remove(id);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void Edit(Inventory inventory) {
            throw new NotImplementedException();
        }

        public void EditInventory(Inventory inventory) {
            try {
                StartTransaction();
                _inventoryRepository.Edit(inventory);
                PersistTransaction();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public List<Inventory> GetAll() {
            try {
                return _inventoryRepository.GetAll().ToList();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Inventory> GetDepletedStock() {
            try {
                return _inventoryRepository.GetDepletedStock();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        #endregion

        #region Methods
        public Inventory GetInventoryByCode(string code) {
            try {
                return _inventoryRepository.GetInventoryByCode(code);
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Inventory> GetInventoryByWarehouse(int warehouseId) {
            try {
                return _inventoryRepository.GetInventoryByWarehouse(warehouseId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Inventory> GetLowInventory() {
            try {
                return _inventoryRepository.GetLowInventory();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Plumber GetPlumber(int Id) {
            try {
                return _inventoryRepository.GetPlumber(Id);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Plumber GetPlumber(string UserId) {
            try {
                return _inventoryRepository.GetPlumber(UserId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<User> GetPlumberUsers() {
            try {
                return _inventoryRepository.GetPlumberUsers();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Supervisor GetSupervisor(string UserId) {
            try {
                return _inventoryRepository.GetSupervisor(UserId);
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public User GetUser(string Id) {
            try {
                return _inventoryRepository.GetUser(Id);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public int[] ImportToDatabase(Plumber p, User user, string filePath, string extension, string firstRowHeader) {
            try {
                StartTransaction();
                var result = _inventoryRepository.ImportToDatabase(p,user,filePath,extension,firstRowHeader);
                PersistTransaction();
                return result;
            }catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Inventory> ListAll() {
            try {
                return _inventoryRepository.GetAll();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        #endregion
    }
}
