namespace Plumbery.Domain.Interfaces.Infrastructure {
    /// <summary>
    /// Interface to handle management of repository
    /// </summary>
    public interface IRepositoryManager {
        /// <summary>
        /// Method to dispose of Context instance
        /// </summary>
        void Finalise();
    }
}
