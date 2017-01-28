namespace Plumbery.Domain.Interfaces.Infrastructure {
    /// <summary>
    /// Interface to perform actions when dataase is modified
    /// </summary>
    public interface IUnitOfWork {
        void SaveChanges();
        void Initialise();
    }
}
