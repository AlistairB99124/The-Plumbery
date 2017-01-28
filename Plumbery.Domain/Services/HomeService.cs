using Plumbery.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Domain;

namespace Plumbery.Domain.Services {
    public class HomeService : ServiceBase, IHomeService {

        #region Private Field

        private IHomeRepository _homeRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise TimeSheet Repository
        /// </summary>
        /// <param name="TimeSheetRepository"></param>
        public HomeService(IHomeRepository homeRepository) {
            _homeRepository = homeRepository;
        }

        #endregion

        #region Methods

        public IEnumerable<Inventory> SearchInventory(List<string> keywords) {
            try {
                return _homeRepository.SearchInventory(keywords);
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Plumber> SearchPlumbers(List<string> keywords) {
            try {
                return _homeRepository.SearchPlumbers(keywords);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Site> SearchSites(List<string> keywords) {
            try {
                return _homeRepository.SearchSites(keywords);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<TimeSheet> SearchTimeSheets(List<string> keywords) {
            try {
                return _homeRepository.SearchTimeSheets(keywords);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Warehouse> SearchWarehouse(List<string> keywords) {
            try {
                return _homeRepository.SearchWarehouse(keywords);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Supervisor> SearchSupervisors(List<string> keywords) {
            try {
                return _homeRepository.SearchSupervisors(keywords);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        #endregion
    }
}
