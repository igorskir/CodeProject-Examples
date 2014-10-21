using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.Repository
{
    /// <remarks>
    /// version 0.2 Chapter II: Repository
    /// </remarks>
    public interface IRepository<TEntity>
    {

        #region CRUD operations
        
        TEntity Save(TEntity instance);
        void Update(TEntity instance);
        void Remove(TEntity instance);
        
        #endregion

        #region Retrieval Operations

        TEntity GetById(long id);
        IQueryable<TEntity> FindAll();
        
        #endregion
    }
}
