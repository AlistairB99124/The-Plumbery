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
        

        public void AddCommentItem(TimeSheetCommentItem comment) {
            try {
                StartTransaction();
                _sheetRepository.AddCommentItem(comment);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void AddMaterialItem(TimeSheetMaterialItem material) {
            try {
                StartTransaction();
                _sheetRepository.AddMaterialItem(material);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void AddTimeSheet(TimeSheet timeSheet) {
            try {
                StartTransaction();
                _sheetRepository.Add(timeSheet);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeductFromInventory(List<TimeSheetMaterialItem> items) {
            try {
                StartTransaction();
                _sheetRepository.DeductFromInventory(items);
                PersistTransaction();
            } catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteCommentItem(int Id) {
            try {
                StartTransaction();
                _sheetRepository.DeleteCommentItem(Id);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteCommentItems(List<TimeSheetCommentItem> items) {
            try {
                StartTransaction();
                _sheetRepository.DeleteCommentItems(items);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteMaterialItem(int Id) {
            try {
                StartTransaction();
                _sheetRepository.DeleteMaterialItem(Id);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteMaterialItems(List<TimeSheetMaterialItem> items) {
            try {
                StartTransaction();
                _sheetRepository.DeleteMaterialItems(items);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void EditCommentItem(TimeSheetCommentItem comment) {
            try {
                StartTransaction();
                _sheetRepository.EditCommentItem(comment);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public void EditMaterialItem(TimeSheetMaterialItem material) {
            try {
                StartTransaction();
                _sheetRepository.EditMaterialItem(material);
                PersistTransaction();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<TimeSheet> GetAll() {
            try {
                return _sheetRepository.GetAll();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Plumber GetPlumber(int Id) {
            try {
                return _sheetRepository.GetPlumber(Id);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Plumber GetPlumber(string UserId) {
            try {
                return _sheetRepository.GetPlumber(UserId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<User> GetPlumberUsers() {
            try {
                return _sheetRepository.GetPlumberUsers();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public Site GetSite(int Id) {
            try {
                return _sheetRepository.GetSite(Id);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public TimeSheet GetTimeSheet(string Code) {
            try {
                return _sheetRepository.GetTimeSheet(Code);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        
        public IEnumerable<TimeSheetCommentItem> ListCommentItems(int TimeSheetId) {
            try {
                return _sheetRepository.ListCommentItems(TimeSheetId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<TimeSheetMaterialItem> ListMaterialItems(int TimeSheetId) {
            try {
                return _sheetRepository.ListMaterialItems(TimeSheetId);
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }
        public IEnumerable<Material> ListMaterials(Plumber plumber) {
            try {
                return _sheetRepository.ListMaterials(plumber);
            }catch(Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Plumber> ListPlumbers() {
            try {
                return _sheetRepository.ListPlumbers();
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Site> ListSites() {
            try {
                return _sheetRepository.ListSites();                    
            } catch (Exception ex) {
                throw new ApplicationException(ex.Message);
            }
        }

      
        #endregion

    }
}
