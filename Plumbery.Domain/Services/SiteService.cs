using Plumbery.Domain.Interfaces.Domain;
using System;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Plumbery.Domain.Services {
    /// <summary>
    /// Service for Site functions
    /// </summary>
    public class SiteService : ServiceBase, ISiteService {
        private ISiteRepository _siteRepository;
        /// <summary>
        /// Initialise Repository
        /// </summary>
        /// <param name="siteRepository"></param>
        public SiteService(ISiteRepository siteRepository) {
            this._siteRepository = siteRepository;
        }
        /// <summary>
        /// Add site to database
        /// </summary>
        /// <param name="site">Site to add</param>
        public async Task<int> Add(Site site, Location location) {
            try {
                StartTransaction();
                _siteRepository.AddSite(site,location);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> AddLocation(Location location) {
            try {
                StartTransaction();
                _siteRepository.AddLocation(location);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Delete site from database
        /// </summary>
        /// <param name="site">Site to delete</param>
        public async Task<int> Delete(Site site) {
            try {
                StartTransaction();
                _siteRepository.Remove(site);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        /// <summary>
        /// Edit site in database
        /// </summary>
        /// <param name="site">Site to be edited</param>
        public async Task<int> Edit(Site site) {
            try {
                StartTransaction();
                _siteRepository.Edit(site);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> EditLocation(Location location) {
            try {
                StartTransaction();
                _siteRepository.EditLocation(location);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Site Get(int? id) {
            try {
                return _siteRepository.Get(Convert.ToInt32(id));
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Site> GetAll() {
            try {
                return _siteRepository.GetAll();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Location GetLocation(int locationId) {
            try {
                return _siteRepository.GetLocation(locationId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// List all sites
        /// </summary>
        public void List() {
            try {
                StartTransaction();
                _siteRepository.GetAll();
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        
    }   
}
