using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;
using eDirectory.Naive.Repository;

namespace eDirectory.Naive.TransManager
{
    /// <remarks>
    /// version 0.4 Chapter IV: Transaction Manager
    /// </remarks>
    public class TransManagerEntityStoreFactory
            : ITransFactory
    {
        private TransManagerEntityStore TransManager;

        #region Implementation of ITransFactory

        public ITransManager CreateManager()
        {
            if (TransManager != null) return TransManager;
            TransManager = new TransManagerEntityStore { Locator = new RepositoryLocatorEntityStore() };
            return TransManager;
        }

        #endregion
    }
}
