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

        TEntity Save<TEntity>(TEntity instance) where TEntity : class;
        void Update<TEntity>(TEntity instance) where TEntity : class;
        void Remove<TEntity>(TEntity instance) where TEntity : class;
        
        #endregion

        #region Retrieval Operations

        TEntity GetById<TEntity>(long id) where TEntity : class;
        IQueryable<TEntity> FindAll<TEntity>() where TEntity : class;

        #endregion

        IRepository<T> GetRepository<T>() where T : class;
        void FlushModifications();

    }
}
