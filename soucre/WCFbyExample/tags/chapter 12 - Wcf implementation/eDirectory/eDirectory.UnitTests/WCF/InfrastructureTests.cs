using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using eDirectory.Common.DependencyInjection;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;
using eDirectory.UnitTests.WCF;
using eDirectory.WPF.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDirectory.UnitTests.WCF
{
    [TestClass]
    public class InfrastructureTests
        :eDirectoryTestBase
    {
        public ServiceAdapter<ITestService> ServiceAdapter;

        [TestInitialize]
        public override void TestsInitialize()
        {
            base.TestsInitialize();
            WcfServiceHost.StartService<TestService, ITestService>();
            ServiceAdapter = new ServiceAdapter<ITestService>();
            ClientServiceLocator.Instance().CommandDispatcher = new WcfServiceHost();
        }

        [TestCleanup]
        public override void TestCleanUp()
        {
            WcfServiceHost.StopService<ITestService>();
            ClientServiceLocator.Instance().CommandDispatcher = (ICommandDispatcher)DiContext.AppContext.GetObject("CommandDispatcherRef");
            base.TestCleanUp();
        }

        [TestMethod]
        public void ServiceReturnsWarning()
        {
            var result = ServiceAdapter.Execute(s => s.MethodReturnsBusinessWarning());
            Assert.IsTrue(result.Response.HasWarning, "A warning was expected");
            Assert.IsTrue(result.Response.BusinessWarnings.Count() == 1, "Only one warning was expected");
            Assert.IsTrue(result.Response.BusinessWarnings.First().Message.Equals("Warning was added"));
        }

        [TestMethod]
        public void ServiceThrowsBusinessException()
        {
            var result = ServiceAdapter.Execute(s => s.MethodThrowsBusinessException());
            Assert.IsTrue(result.Response.HasException, "An exception was expected");
            Assert.IsTrue(result.Response.BusinessException.Message.Equals("Business Exception was thrown"));
        }

        [TestMethod, ExpectedException(typeof(FaultException))]
        public void ServiceThrowsApplicationException()
        {
            ServiceAdapter.Execute(s => s.MethodReturnsApplicationException());
        }

    }
}
