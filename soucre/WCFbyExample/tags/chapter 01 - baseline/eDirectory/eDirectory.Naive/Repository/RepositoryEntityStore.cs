using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;

namespace eDirectory.Naive.Repository
{
    /// <remarks>
    /// version 0.1 Chapter: Introduction
    /// </remarks>
    public abstract class RepositoryEntityStore<TEntity>
        :IRepository<TEntity>
    {
        protected readonly IDictionary<long, TEntity> RepositoryMap = new Dictionary<long, TEntity>();

        #region IRepository<TEntity> Members

        public abstract TEntity Save(TEntity instance);

        public abstract void Update(TEntity instance);

        public abstract void Remove(TEntity instance);

        public TEntity GetById(long id)
        {
            return RepositoryMap[id];
        }

        public IQueryable<TEntity> FindAll()
        {
            return RepositoryMap.Values.AsQueryable();
        }

        #endregion
    }
}
