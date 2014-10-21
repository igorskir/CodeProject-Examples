using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using NHibernate;
using eDirectory.NHibernate.BootStrapper;

namespace eDirectory.NHibernate.Repository
{
    public class RepositoryLocatorNh
        :RepositoryLocatorBase, IStoreInitialiser
    {
        private readonly ISession _sessionInstance;
        private readonly string _configurationName;

        public RepositoryLocatorNh(ISession session, string configurationName)
        {
            _sessionInstance = session;
            _configurationName = configurationName;
        }
        
        #region Overrides of RepositoryLocatorBase

        protected override IRepository<T> CreateRepository<T>()
        {
            return new RepositoryNh<T>(_sessionInstance);
        }

        #endregion

        #region Implementation of IStoreInitialiser

        public void ConfigureStore()
        {
            var nhBootStrapper = new NhBootStrapper { ConfigurationFileName = _configurationName };
            nhBootStrapper.eDirectorySchemaExport.Create(false, true);
        }

        #endregion
    }
}
