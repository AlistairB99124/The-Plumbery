using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;

namespace Plumbery.Domain.Services {
    public class SupervisorService : ServiceBase, ISupervisorService {
        #region Private Field

        private ISupervisorRepository _supervisorRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise User Repository
        /// </summary>
        /// <param name="userRepository"></param>
        public SupervisorService(ISupervisorRepository supervisorRepository) {
            _supervisorRepository = supervisorRepository;
        }

        public void AddSupervisor(Supervisor Supervisor) {
            try {
                StartTransaction();
                _supervisorRepository.Add(Supervisor);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteSupervisor(int id) {
            try {
                StartTransaction();
                _supervisorRepository.Remove(id);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void EditSupervisor(Supervisor Supervisor) {
            try {
                StartTransaction();
                _supervisorRepository.Edit(Supervisor);
                PersistTransaction();
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Supervisor> GetAll() {
            try {
                return _supervisorRepository.GetAll();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Supervisor GetSupervisor(string v) {
            try {
                return _supervisorRepository.Get(v);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Supervisor GetSupervisor(int? id) {
            try {
                return _supervisorRepository.Get(Convert.ToInt32(id));
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<User> GetUsers() {
            try {
                return _supervisorRepository.GetUsers();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        #endregion

        #region Methods



        #endregion
    }
}
