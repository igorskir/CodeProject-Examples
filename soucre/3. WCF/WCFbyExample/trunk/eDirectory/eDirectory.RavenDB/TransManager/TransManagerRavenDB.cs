using Raven.Client;
using eDirectory.Domain.TransManager;
using eDirectory.RavenDB.Repository;

namespace eDirectory.RavenDB.TransManager
{
    public class TransManagerRavenDB
        : TransManagerBase
    {
        private readonly IDocumentSession _sessionInstance;

        public TransManagerRavenDB(IDocumentSession session)
        {
            _sessionInstance = session;
            Locator = new RepositoryLocatorRavenDB(_sessionInstance);
        }

        #region Overriden Base Methods

        public override void BeginTransaction()
        {
            base.BeginTransaction();            
        }

        public override void CommitTransaction()
        {
            base.CommitTransaction();
            _sessionInstance.SaveChanges();
        }

        public override void Rollback()
        {
            base.Rollback();
            _sessionInstance.Advanced.Clear();
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
            if (!_sessionInstance.Advanced.HasChanges) return;
            _sessionInstance.Advanced.Clear();
            _sessionInstance.Dispose();
        }

        #endregion
    }
}