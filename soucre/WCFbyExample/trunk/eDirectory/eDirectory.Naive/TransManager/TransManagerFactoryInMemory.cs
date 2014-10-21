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
    /// version 0.10 Chapter X: Renamed from TransManagerEntityStoreFactory to TransManagerFactoryInMemory
    /// </remarks>
    public class TransManagerFactoryInMemory
            : ITransFactory
    {
        private TransManagerInMemory _transManager;

        #region Implementation of ITransFactory

        public ITransManager CreateManager()
        {
            if (_transManager != null) return _transManager;
            _transManager = new TransManagerInMemory ();
            return _transManager;
        }

        #endregion
    }
}
