using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.Repository
{
    /// <remarks>
    /// version 0.2 Chapter II: Repository
    /// </remarks>
    public abstract class RepositoryLocatorBase
        : IRepositoryLocator
    {

        #region IRepositoryLocator Members

        public TEntity Save<TEntity>(TEntity instance)
        {
            return GetRepository<TEntity>().Save(instance);
        }

        public void Update<TEntity>(TEntity instance)
        {
            GetRepository<TEntity>().Update(instance);
        }

        public void Remove<TEntity>(TEntity instance)
        {
            GetRepository<TEntity>().Remove(instance);
        }

        public TEntity GetById<TEntity>(long id)
        {
            return GetRepository<TEntity>().GetById(id);
        }

        public IQueryable<TEntity> FindAll<TEntity>()
        {
            return GetRepository<TEntity>().FindAll();
        }

        public abstract IRepository<T> GetRepository<T>();

        #endregion
    }
}
