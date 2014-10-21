using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using System.Data.Entity;

namespace eDirectory.EF.Repository
{
    public class RepositoryEF<TEntity>
        : IRepository<TEntity> where TEntity : class
    {
        private readonly IDbSet<TEntity> _dbSet;

        public RepositoryEF(IDbSet<TEntity> dbSet)
        {
            _dbSet = dbSet;
        }
        
        #region Implementation of IRepository<TEntity>

        public TEntity Save(TEntity instance)
        {
            return _dbSet.Add(instance);
        }

        public void Update(TEntity instance)
        {
        }

        public void Remove(TEntity instance)
        {
            _dbSet.Remove(instance);
        }

        public TEntity GetById(long id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> FindAll()
        {
            return _dbSet.AsQueryable();
        }

        #endregion
    }
}
