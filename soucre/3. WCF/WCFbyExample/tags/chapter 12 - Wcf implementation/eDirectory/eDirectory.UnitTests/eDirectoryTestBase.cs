using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.DependencyInjection;
using eDirectory.Common.Message;
using eDirectory.Domain.AppServices;
using eDirectory.Domain.Repository;
using eDirectory.Domain.TransManager;
using eDirectory.NHibernate.BootStrapper;
using eDirectory.NHibernate.TransManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDirectory.UnitTests
{
    [TestClass]
    [DeploymentItem("nhibernate.cfg.xml")]
    public abstract class eDirectoryTestBase
    {
        [TestInitialize]
        public virtual void TestsInitialize()
        {
            InitialiseDatabase();
        }

        private static void InitialiseDatabase()
        {
            var nHFactory = GlobalContext.Instance().TransFactory as TransManagerFactoryNh;
            if (nHFactory == null) return;
            var nhBootStrapper = new NhBootStrapper {ConfigurationFileName = nHFactory.ConfigurationFileName};
            nhBootStrapper.eDirectorySchemaExport.Create(false, true);
        }

        [TestCleanup]
        public virtual void TestCleanUp()
        {
            ResetLocator();
        }

        private static void ResetLocator()
        {            
            using (ITransManager manager = GlobalContext.Instance().TransFactory.CreateManager())
            {
                manager.ExecuteCommand(locator =>
                                           {
                                               var resetable = locator as IResetable;
                                               if (resetable == null) return null;
                                               resetable.Reset();
                                               return new DtoResponse();
                                           });
            }
        }
    }
}
