using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
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
        public bool IsIisEnvironment { get; set; }

        #endregion

        #region Session Factory Initializer

        private ISessionFactory InitializeSessionFactory()
        {
            var nhConfiguration = new NhBootStrapper {ConfigurationFileName = Translate(ConfigurationFileName)};
            return nhConfiguration.NhConfiguration.BuildSessionFactory();
        }

        private string Translate(string fileName)
        {
            if (!IsIisEnvironment) return fileName;
            var name = HttpRuntime.AppDomainAppPath + fileName;
            return name;
        } 

        #endregion
    }
}
