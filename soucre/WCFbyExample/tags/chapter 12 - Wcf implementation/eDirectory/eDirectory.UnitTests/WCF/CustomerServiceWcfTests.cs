using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.DependencyInjection;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;
using eDirectory.Domain.Services;
using eDirectory.UnitTests.BootStrapper;
using eDirectory.UnitTests.Domain.Services;
using eDirectory.WPF.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDirectory.UnitTests.WCF
{
    [TestClass]
    public class CustomerServiceWcfTests
        : CustomerServiceTests
    {
        [TestInitialize]
        public override void TestsInitialize()
        {
            base.TestsInitialize();
            WcfServiceHost.StartService<CustomerService, ICustomerService>();
            ClientServiceLocator.Instance().CommandDispatcher = new WcfServiceHost();
        }

        [TestCleanup]
        public override void TestCleanUp()
        {
            WcfServiceHost.StopService<ICustomerService>();
            base.TestCleanUp();
            ClientServiceLocator.Instance().CommandDispatcher = (ICommandDispatcher) DiContext.AppContext.GetObject("CommandDispatcherRef");
        }

    }
}
