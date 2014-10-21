using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Domain.Repository;
using eDirectory.Domain.AppServices;

namespace eDirectory.Domain.TransManager
{
    /// <remarks>
    /// version 0.40 Chapter IV: Transaction Manager
    /// version 0.71 Context Re-Factor
    /// </remarks>
    public abstract class TransManagerBase
        :ITransManager
    {
        protected bool IsInTranx;

        public IRepositoryLocator Locator { get; set; }

        #region ITransManager Members

        public TResult ExecuteCommand<TResult>(Func<Repository.IRepositoryLocator, TResult> command) 
            where TResult : class, Common.Message.IDtoResponseEnvelop
        {
            try
            {
                BeginTransaction();
                var result = command.Invoke(Locator);
                CommitTransaction();
                CheckForWarnings(result);
                return result;
            }
            catch (BusinessException exception)
            {
                if (IsInTranx) Rollback();
                var type = typeof(TResult);
                var instance = Activator.CreateInstance(type, true) as IDtoResponseEnvelop;
                if (instance != null) instance.Response.AddBusinessException(exception);
                return instance as TResult;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual void BeginTransaction()
        {
            IsInTranx = true;
            return;
        }

        public virtual void CommitTransaction()
        {
            IsInTranx = false;
            return;
        }

        public virtual void Rollback()
        {
            IsInTranx = false;
            return;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected bool IsDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            // free managed resources
            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            Locator = null;
            IsDisposed = true;
        }

        #endregion

        #region Helper Methods

        private void CheckForWarnings<TResult>(TResult result)
        {
            var response = result as IDtoResponseEnvelop;
            if (response == null) return;
            var notifier = Container.RequestContext.Notifier;
            if (notifier.HasWarnings) response.Response.AddBusinessWarnings(notifier.RetrieveWarnings());
        }

        #endregion
    }
}
