using Microsoft.Practices.ServiceLocation;
using Plumbery.Domain.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Plumbery.Domain.Services {
    /// <summary>
    /// Class to handle start and end process of interaction with database 
    /// </summary>
    public class ServiceBase {
        private IUnitOfWork _unitOfWork;
        /// <summary>
        /// Initialise database for changes to be made
        /// </summary>
        public virtual void StartTransaction() {
            _unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            _unitOfWork.Initialise();
        }
        /// <summary>
        /// Save changes made to databse
        /// </summary>
        public virtual void PersistTransaction() {
            _unitOfWork.SaveChanges();
        }

        public virtual async Task<int> PersistTransactionAsync() => await _unitOfWork.SaveChangesAsync();
        
    }
}
