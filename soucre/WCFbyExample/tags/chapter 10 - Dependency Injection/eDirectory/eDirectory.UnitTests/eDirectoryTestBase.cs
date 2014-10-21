using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.DependencyInjection;
using eDirectory.Common.Message;
using eDirectory.Domain.AppServices;
using eDirectory.Domain.Repository;
using eDirectory.Domain.TransManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDirectory.UnitTests
{
    [TestClass]
    public abstract class eDirectoryTestBase
    {
        [TestInitialize]
        public virtual void TestsInitialize()
        {
            
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
