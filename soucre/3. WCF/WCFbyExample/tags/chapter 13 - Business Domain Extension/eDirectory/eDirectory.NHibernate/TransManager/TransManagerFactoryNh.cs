using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Entities;
using eDirectory.Domain.TransManager;
using eDirectory.NHibernate.BootStrapper;
using NHibernate;
using NHibernate.Cfg;

namespace eDirectory.NHibernate.TransManager
{
    public class TransManagerFactoryNh
        : ITransFactory
    {

        private ISessionFactory SessionFactoryInstance;

        private ISessionFactory SessionFactory
        {
            get
            {
                if (SessionFactoryInstance != null) return SessionFactoryInstance;
                SessionFactoryInstance = InitializeSessionFactory();
                return SessionFactoryInstance;
            }
        }

        #region Implementation of ITransFactory

        public ITransManager CreateManager()
        {
            return new TransManagerNh(SessionFactory.OpenSession());
        }

        #endregion

        #region Properties

        public string ConfigurationFileName { get; set; }

        #endregion

        #region Session Factory Initializer

        private ISessionFactory InitializeSessionFactory()
        {
            var nhConfiguration = new NhBootStrapper {ConfigurationFileName = this.ConfigurationFileName};
            return nhConfiguration.NhConfiguration.BuildSessionFactory();
        }

        #endregion
    }
}
