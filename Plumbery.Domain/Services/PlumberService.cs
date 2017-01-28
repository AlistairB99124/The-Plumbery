using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;

namespace Plumbery.Domain.Services {
    public class PlumberService : ServiceBase, IPlumberService {
        #region Private Field

        private IPlumberRepository _plumberRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise TimeSheet Repository
        /// </summary>
        /// <param name="TimeSheetRepository"></param>
        public PlumberService(IPlumberRepository plumberRepository) {
            _plumberRepository = plumberRepository;
        }

        public void AddPlumber(Plumber plumber) {
            try {
                StartTransaction();
                _plumberRepository.Add(plumber);
                PersistTransaction();
            }catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeletePlumber(int id) {
            try {
                StartTransaction();
                _plumberRepository.Remove(id);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void EditPlumber(Plumber plumber) {
            try {
                StartTransaction();
                _plumberRepository.Edit(plumber);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Plumber GetPlumber(int? Id) {
            try {
                return _plumberRepository.Get(Convert.ToInt32(Id));
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Plumber> GetPlumbers() {
            try {
                return _plumberRepository.GetAll();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Supervisor GetSupervisor(string UserId) {
            try {
                return _plumberRepository.GetSupervisor(UserId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        #endregion

        #region Methods
        public IEnumerable<User> GetUsers() {
            try {
                return _plumberRepository.GetUsers();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Warehouse> GetWarehouses() {
            try {
                return _plumberRepository.GetWarehouses();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }



        #endregion
    }
}
