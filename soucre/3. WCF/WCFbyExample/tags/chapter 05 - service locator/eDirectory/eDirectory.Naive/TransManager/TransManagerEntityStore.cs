using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;

namespace eDirectory.Naive.TransManager
{
    /// <remarks>
    /// version 0.4 Chapter IV: Transaction Manager
    /// </remarks>
    public class TransManagerEntityStore
        : TransManagerBase
    {
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
