using Plumbery.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Entities;
using Plumbery.Domain.Interfaces.Repositories;

namespace Plumbery.Domain.Services {
    public class TimeSheetService : ServiceBase, ITimeSheetService {
        #region Private Field

        private ITimeSheetRepository _sheetRepository;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise TimeSheet Repository
        /// </summary>
        /// <param name="TimeSheetRepository"></param>
        public TimeSheetService(ITimeSheetRepository sheetRepository) {
            _sheetRepository = sheetRepository;
        }

        #endregion

        #region Methods
        

        public async Task<int> AddCommentItem(TimeSheetCommentItem comment) {
            try {
                StartTransaction();
                _sheetRepository.AddCommentItem(comment);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> AddMaterialItem(TimeSheetMaterialItem material) {
            try {
                StartTransaction();
                _sheetRepository.AddMaterialItem(material);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> AddTimeSheet(TimeSheet timeSheet) {
            try {
                StartTransaction();
                _sheetRepository.Add(timeSheet);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> DeductFromInventory(List<TimeSheetMaterialItem> items) {
            try {
                StartTransaction();
                _sheetRepository.DeductFromInventory(items);
                return await PersistTransactionAsync();
            } catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> DeleteCommentItem(int Id) {
            try {
                StartTransaction();
                _sheetRepository.DeleteCommentItem(Id);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> DeleteCommentItems(List<TimeSheetCommentItem> items) {
            try {
                StartTransaction();
                _sheetRepository.DeleteCommentItems(items);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> DeleteMaterialItem(int Id) {
            try {
                StartTransaction();
                _sheetRepository.DeleteMaterialItem(Id);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> DeleteMaterialItems(List<TimeSheetMaterialItem> items) {
            try {
                StartTransaction();
                _sheetRepository.DeleteMaterialItems(items);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> DeleteSheet(int id) {
            try {
                StartTransaction();
                _sheetRepository.DeleteSheet(id);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> EditCommentItem(TimeSheetCommentItem comment) {
            try {
                StartTransaction();
                _sheetRepository.EditCommentItem(comment);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<int> EditMaterialItem(TimeSheetMaterialItem material) {
            try {
                StartTransaction();
                _sheetRepository.EditMaterialItem(material);
                return await PersistTransactionAsync();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<TimeSheet>> GetAll() {
            try {
                return await _sheetRepository.GetAllTimeSheets();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<Plumber> GetPlumber(int Id) {
            try {
                return await _sheetRepository.GetPlumber(Id);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<Plumber> GetPlumber(string UserId) {
            try {
                return await _sheetRepository.GetPlumber(UserId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<User>> GetPlumberUsers() {
            try {
                return await _sheetRepository.GetPlumberUsers();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<Site> GetSite(int Id) {
            try {
                return await _sheetRepository.GetSite(Id);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<TimeSheet> GetTimeSheet(string Code) {
            try {
                return await _sheetRepository.GetTimeSheet(Code);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        
        public async Task<IEnumerable<TimeSheetCommentItem>> ListCommentItems(int TimeSheetId) {
            try {
                return await _sheetRepository.ListCommentItems(TimeSheetId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<TimeSheetMaterialItem>> ListMaterialItems(int TimeSheetId) {
            try {
                return await _sheetRepository.ListMaterialItems(TimeSheetId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<IEnumerable<Material>> ListMaterials(Plumber plumber) {
            try {
                return await _sheetRepository.ListMaterials(plumber);
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Plumber>> ListPlumbers() {
            try {
                return await _sheetRepository.ListPlumbers();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Site>> ListSites() {
            try {
                return await _sheetRepository.ListSites();                    
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

      
        #endregion

    }
}
