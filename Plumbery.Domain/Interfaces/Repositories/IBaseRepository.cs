using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plumbery.Domain.Interfaces.Repositories {
    /// <summary>
    /// Base interface handling CRUD functionailty
    /// </summary>
    /// <typeparam name="TEntity">Entity to be Handled</typeparam>
    public interface IBaseRepository<TEntity> where TEntity : class {
        /// <summary>
        /// Get generic list of all of type TEntity
        /// </summary>
        /// <returns>List of TEntity</returns>
        IList<TEntity> GetAll();
        /// <summary>
        /// Get TEntity by Id
        /// </summary>
        /// <param name="Id">Id to look up</param>
        /// <returns>TEntity</returns>
        TEntity Get(int Id);
        /// <summary>
        /// Add TEntity to database
        /// </summary>
        /// <param name="obj">TEntity to add</param>
        void Add(TEntity obj);
        /// <summary>
        /// Edit TEntity in database
        /// </summary>
        /// <param name="obj">TEntity to Edit</param>
        void Edit(TEntity obj);
        /// <summary>
        /// Remove TEntity from database
        /// </summary>
        /// <param name="obj">TEntity to remove</param>
        void Remove(TEntity obj);
        /// <summary>
        /// Remove TEntity by Id from database
        /// </summary>
        /// <param name="Id">Id of TEntity to Remove</param>
        void Remove(int Id);
    }
}
