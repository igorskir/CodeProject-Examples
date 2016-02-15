using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;
using eDirectory.Naive.Repository;

namespace eDirectory.Naive.TransManager
{
    /// <remarks>
    /// version 0.04 Chapter IV: Transaction Manager
    /// version 0.10 Chapter X: Renamed from TransManagerEntityStore to TransManagerInMemory
    /// </remarks>
    public class TransManagerInMemory
        : TransManagerBase
    {
        public TransManagerInMemory()
        {
            Locator = new RepositoryLocatorInMemory();
        }

        #region Overrides of TransManager

        /// <summary>
        /// Need to override this method because
        /// we want to keep the transmanager in the
        /// Entity Store implementation as instances
        /// are stored in memory
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (!disposing) return;
            // free managed resources
            if (!IsDisposed && IsInTranx)
            {
                Rollback();
            }
            //Locator = null;
            IsDisposed = true;
        }

        #endregion
    }
}
