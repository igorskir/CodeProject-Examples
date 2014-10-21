using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using NHibernate;
using NHibernate.Linq;

namespace eDirectory.NHibernate.Repository
{
    /// <remarks>
    /// version 0.11 Chapter XI
    /// </remarks>
    public class RepositoryNh<TEntity>
        : IRepository<TEntity>
    {

        private readonly ISession SessionInstance;

        public RepositoryNh(ISession session)
        {
            SessionInstance = session;
        }

        #region Implementation of IRepository<TEntity>

        public TEntity Save(TEntity instance)
        {
            SessionInstance.Save(instance);
            return instance;
        }

        public void Update(TEntity instance)
        {
            SessionInstance.Update(instance);
        }

        public void Remove(TEntity instance)
        {
            SessionInstance.Delete(instance);
        }

        public TEntity GetById(long id)
        {
            return SessionInstance.Get<TEntity>(id);
        }

        public IQueryable<TEntity> FindAll()
        {
            return SessionInstance.Linq<TEntity>();
        }

        #endregion
    }
}
