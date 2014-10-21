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
  using Common.Message;
  using WPF.Services;

  [TestClass]
  public class InfrastructureTests
    : eDirectoryTestBase
  {
    private ICommandDispatcher _originalCommandDispatcher;
    public ServiceAdapter<ITestService> TestServiceAdapter { get; set; }

    [TestInitialize]
    public override void TestsInitialize()
    {
      base.TestsInitialize();
      TestServiceAdapter = new ServiceAdapter<ITestService>();
      WcfServiceHost.StartService<TestService, ITestService>();
      _originalCommandDispatcher = ClientServiceLocator.Instance().CommandDispatcher;
      ClientServiceLocator.Instance().CommandDispatcher = new WcfTestCommandDispatcher();
    }

    [TestCleanup]
    public override void TestCleanUp()
    {
      WcfServiceHost.StopService<ITestService>();
      ClientServiceLocator.Instance().CommandDispatcher = _originalCommandDispatcher;
      base.TestCleanUp();
    }

    [TestMethod]
    public void ServiceReturnsWarning()
    {
      var result = TestServiceAdapter.Execute(service => service.MethodReturnsBusinessWarning());
      Assert.IsTrue(result.Response.HasWarning, "A warning was expected");
      Assert.IsTrue(result.Response.BusinessWarnings.Count() == 1, "Only one warning was expected");
      Assert.IsTrue(result.Response.BusinessWarnings.First().Message.Equals("Warning was added"));
    }

    [TestMethod]
    public void ServiceThrowsBusinessException()
    {
      var result = TestServiceAdapter.Execute(service => service.MethodThrowsBusinessException());
      Assert.IsTrue(result.Response.HasException, "An exception was expected");
      Assert.IsTrue(result.Response.BusinessException.Message.Equals("Business Exception was thrown"));
    }

    //[TestMethod, ExpectedException(typeof(eDirectory.Common.Message.BusinessException), AllowDerivedTypes = true)]
    //public void ServiceThrowsApplicationException()
    //{
    //  TestServiceAdapter.Execute(service => service.MethodReturnsApplicationException());
    //}

  }
}
