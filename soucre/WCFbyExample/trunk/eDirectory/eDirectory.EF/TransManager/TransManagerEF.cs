﻿using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using eDirectory.Domain.TransManager;
using eDirectory.EF.Repository;

namespace eDirectory.EF.TransManager
{
    public class TransManagerEF
        : TransManagerBase
    {
        private readonly DbContext _dbContext;
        private DbTransaction _transaction;

        public TransManagerEF(DbContext dbContext)
        {
            _dbContext = dbContext;
            var locator = new RepositoryLocatorEF(_dbContext);
            Locator = locator;
        }
       
        public override void BeginTransaction()
        {
            base.BeginTransaction();
            if (_dbContext.GetObjectContext().Connection.State != ConnectionState.Open)
            {
                _dbContext.GetObjectContext().Connection.Open();
            }
            _transaction = _dbContext.GetObjectContext().Connection.BeginTransaction();
        }

        public override void CommitTransaction()
        {
            base.CommitTransaction();
            _dbContext.GetObjectContext().SaveChanges(SaveOptions.AcceptAllChangesAfterSave | SaveOptions.DetectChangesBeforeSave);
            _dbContext.GetObjectContext().AcceptAllChanges();    
            _transaction.Commit();
          return;
          _transaction.Rollback();
        }

        public override void Rollback()
        {
            base.Rollback();            
            _transaction.Rollback();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            // free managed resources
            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            Close();
            Locator = null;
            IsDisposed = true;
        }

        private void Close()
        {
            if (_dbContext == null) return;
            if (_dbContext.Database.Connection.State != ConnectionState.Open) return;
            _dbContext.Database.Connection.Close();
        }
    }
}
