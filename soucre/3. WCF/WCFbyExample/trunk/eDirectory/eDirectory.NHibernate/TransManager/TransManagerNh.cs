using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;
using eDirectory.NHibernate.Repository;
using NHibernate;

namespace eDirectory.NHibernate.TransManager
{
    public class TransManagerNh
        :TransManagerBase
    {
        private readonly ISession _sessionInstance;

        public TransManagerNh(ISession session, string configurationName)
        {
            _sessionInstance = session;
            Locator = new RepositoryLocatorNh(_sessionInstance, configurationName);
        }

        #region Overriden Base Methods

        public override void BeginTransaction()
        {
            base.BeginTransaction();
            if (_sessionInstance.Transaction.IsActive) return;
            _sessionInstance.BeginTransaction();
        }

        public override void CommitTransaction()
        {
            base.CommitTransaction();
            if (!_sessionInstance.Transaction.IsActive) return;
            _sessionInstance.Transaction.Commit();
        }

        public override void Rollback()
        {
            base.Rollback();
            if (!_sessionInstance.Transaction.IsActive) return;
            _sessionInstance.Transaction.Rollback();
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
            if (_sessionInstance == null) return;
            if (!_sessionInstance.IsOpen) return;
            _sessionInstance.Close();
        }

        #endregion
    }
}
