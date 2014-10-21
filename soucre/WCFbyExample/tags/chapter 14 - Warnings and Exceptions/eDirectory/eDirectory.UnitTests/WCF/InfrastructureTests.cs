using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using eDirectory.Common.ServiceContract;
using eDirectory.UnitTests.WCF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDirectory.UnitTests.WCF
{
    [TestClass]
    public class InfrastructureTests
        :eDirectoryTestBase
    {
        [TestInitialize]
        public override void TestsInitialize()
        {
            base.TestsInitialize();
            WcfServiceHost.StartService<TestService, ITestService>();
        }

        [TestCleanup]
        public override void TestCleanUp()
        {
            WcfServiceHost.StopService<ITestService>();
            base.TestCleanUp();
        }

        [TestMethod]
        public void ServiceReturnsWarning()
        {
            WcfServiceHost.InvokeService<ITestService>(ServiceReturnsWarningCommand);
        }

        private void ServiceReturnsWarningCommand(ITestService service)
        {
            var result = service.MethodReturnsBusinessWarning();
            Assert.IsTrue(result.Response.HasWarning, "A warning was expected");
            Assert.IsTrue(result.Response.BusinessWarnings.Count() == 1, "Only one warning was expected");
            Assert.IsTrue(result.Response.BusinessWarnings.First().Message.Equals("Warning was added"));
        }

        [TestMethod]
        public void ServiceThrowsBusinessException()
        {
            WcfServiceHost.InvokeService<ITestService>(ServiceThrowsBusinessExceptionCommand);            
        }

        private void ServiceThrowsBusinessExceptionCommand(ITestService service)
        {
            var result = service.MethodThrowsBusinessException();
            Assert.IsTrue(result.Response.HasException, "An exception was expected");
            Assert.IsTrue(result.Response.BusinessException.Message.Equals("Business Exception was thrown"));
        }

        [TestMethod, ExpectedException(typeof(FaultException))]
        public void ServiceThrowsApplicationException()
        {
            WcfServiceHost.InvokeService<ITestService>(ServiceThrowsApplicationExceptionCommand);
        }

        private void ServiceThrowsApplicationExceptionCommand(ITestService service)
        {
            service.MethodReturnsApplicationException();
        }

    }
}
