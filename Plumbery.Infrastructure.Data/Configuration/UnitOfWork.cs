using Microsoft.Practices.ServiceLocation;
using Plumbery.Domain.Interfaces.Infrastructure;
using Plumbery.Infrastructure.Data.Context;

namespace Plumbery.Infrastructure.Data.Configuration {
    /// <summary>
    /// Class to handle initialisation of context and save changes to database
    /// </summary>
    public class UnitOfWork : IUnitOfWork {

        /// <summary>
        /// Instance of Context
        /// </summary>
        private ContextBank _context;
        /// <summary>
        /// Initialise context
        /// </summary>
        public void Initialise() {
            var manager = ServiceLocator.Current.GetInstance<IRepositoryManager>() as RepositoryManager;
            _context = manager.Context;
        }
        /// <summary>
        /// Save changes to database
        /// </summary>
        public void SaveChanges() {
            _context.SaveChanges();
        }
    }
}
