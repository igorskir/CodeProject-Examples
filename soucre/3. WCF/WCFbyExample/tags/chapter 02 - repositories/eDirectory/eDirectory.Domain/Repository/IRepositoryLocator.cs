using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.Repository
{
    /// <remarks>
    /// version 0.2 Chapter II: Repository
    /// </remarks>
    public interface IRepositoryLocator
    {
        #region CRUD operations
        
        TEntity Save<TEntity>(TEntity instance);
        void Update<TEntity>(TEntity instance);
        void Remove<TEntity>(TEntity instance);
        
        #endregion

        #region Retrieval Operations

        TEntity GetById<TEntity>(long id);
        IQueryable<TEntity> FindAll<TEntity>();

        #endregion

        IRepository<T> GetRepository<T>();

    }
}
