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
        protected Dictionary<Type, object> RepositoryMap = new Dictionary<Type, object>();

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

        public IRepository<T> GetRepository<T>()
        {
            var type = typeof(T);
            if (RepositoryMap.Keys.Contains(type)) return RepositoryMap[type] as IRepository<T>;
            var repository = CreateRepository<T>();
            RepositoryMap.Add(type, repository);
            return repository;
        }

        protected abstract IRepository<T> CreateRepository<T>();

        #endregion
    }
}
