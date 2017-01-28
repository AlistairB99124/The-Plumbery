using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plumbery.Domain.Interfaces.Repositories;
using Plumbery.Infrastructure.Data.Context;
using Plumbery.Infrastructure.Data.Configuration;
using Plumbery.Domain.Interfaces.Infrastructure;
using Microsoft.Practices.ServiceLocation;
using System.Data.Entity;

namespace Plumbery.Infrastructure.Data.Repositories {
    /// <summary>
    /// Base class handling CRUD functionality
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class {

        #region Private Fields
        /// <summary>
        /// Protected instance of the context
        /// </summary>
        protected readonly ContextBank _context;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor to initialise Context
        /// </summary>
        public BaseRepository() {
            var manager = (RepositoryManager)ServiceLocator.Current.GetInstance<IRepositoryManager>();
            _context = manager.Context;
        }

        #endregion


        #region Methods
        /// <summary>
        /// Add Object
        /// </summary>
        /// <param name="obj"></param>
        public void Add(TEntity obj) {
            _context.Set<TEntity>().Add(obj);
        }
        /// <summary>
        /// Edit TEntity in database
        /// </summary>
        /// <param name="obj">TEntity to Edit</param>
        public void Edit(TEntity obj) {
            _context.Entry(obj).State = EntityState.Modified;
        }
        /// <summary>
        /// Get TEntity by Id
        /// </summary>
        /// <param name="Id">Id to look up</param>
        /// <remarks>WARNING! Cannot Get User by (int)ID. There is a separate method for that</remarks>
        /// <returns>TEntity</returns>
        public TEntity Get(int Id) => _context.Set<TEntity>().Find(Id);
        /// <summary>
        /// Get generic list of all of type TEntity
        /// </summary>
        /// <returns>List of TEntity</returns>
        public IList<TEntity> GetAll() => _context.Set<TEntity>().ToList();
        /// <summary>
        /// Remove TEntity by Id from database
        /// </summary>
        /// <remarks>WARNING! Cannot use this method for User. Separate method required</remarks>
        /// <param name="Id">Id of TEntity to Remove</param>
        public void Remove(int Id) {
            TEntity obj = Get(Id);
            Remove(obj);
        }
        /// <summary>
        /// Remove TEntity from database
        /// </summary>
        /// <param name="obj">TEntity to remove</param>
        public void Remove(TEntity obj) {
            _context.Set<TEntity>().Remove(obj);
        }

        #endregion
    }
}
