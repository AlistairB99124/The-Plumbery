using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;
using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Services {
    public class WarehouseService:ServiceBase, IWarehouseService {
        private IWarehouseRepository _warehouseRepository;
        /// <summary>
        /// Initialise Repository
        /// </summary>
        /// <param name="warehouseRepository"></param>
        public WarehouseService(IWarehouseRepository warehouseRepository) {
            this._warehouseRepository = warehouseRepository;
        }
        /// <summary>
        /// Add warehouse to database
        /// </summary>
        /// <param name="warehouse">Warehouse to add</param>
        public async Task<int> Add(Warehouse warehouse) {
            try {
                StartTransaction();
                _warehouseRepository.Add(warehouse);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Delete warehouse from database
        /// </summary>
        /// <param name="warehouse">Warehouse to delete</param>
        public async Task<int> Delete(Warehouse warehouse) {
            try {
                StartTransaction();
                _warehouseRepository.Remove(warehouse);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// Edit warehouse in database
        /// </summary>
        /// <param name="warehouse">Warehouse to be edited</param>
        public async Task<int> Edit(Warehouse warehouse) {
            try {
                StartTransaction();
                _warehouseRepository.Edit(warehouse);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Warehouse Get(int? id) {
            try {
                return _warehouseRepository.Get(Convert.ToInt32(id));
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Warehouse> GetAll() {
            try {
                return _warehouseRepository.GetAll();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// List all warehouses
        /// </summary>
        public void List() {
            try {
                StartTransaction();
                _warehouseRepository.GetAll();
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
