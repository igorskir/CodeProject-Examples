using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using eDirectory.EF.TransManager;

namespace eDirectory.EF.Repository
{
    public class RepositoryLocatorEF
        : RepositoryLocatorBase
    {
        private readonly DbContext _dbContext;

        public RepositoryLocatorEF(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Overrides of RepositoryLocatorBase

        public override void FlushModifications()
        {
            base.FlushModifications();
            _dbContext.GetObjectContext().SaveChanges(SaveOptions.DetectChangesBeforeSave | SaveOptions.AcceptAllChangesAfterSave);
        }

        protected override IRepository<TEntity> CreateRepository<TEntity>()
        {
            return new RepositoryEF<TEntity>(_dbContext.Set<TEntity>());
        }

        #endregion
    }
}
