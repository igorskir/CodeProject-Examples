using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.ServiceContract;
using eDirectory.Domain.Services;
using eDirectory.UnitTests.Domain.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDirectory.UnitTests.WCF
{
  using Common.Message;
  using WPF.Services;

  /// <summary>
  /// version 0.13 Chapter XIII: Business Domain Extension
  /// </summary>
  [TestClass]
  public class CustomerServiceWcfTests
    : CustomerServiceTests
  {
    private ICommandDispatcher _originalCommandDispatcher;

    [TestInitialize]
    public override void TestsInitialize()
    {
      base.TestsInitialize();
      WcfServiceHost.StartService<CustomerService, ICustomerService>();
      WcfServiceHost.StartService<AddressService, IAddressService>();
      _originalCommandDispatcher = ClientServiceLocator.Instance().CommandDispatcher;
      ClientServiceLocator.Instance().CommandDispatcher = new WcfTestCommandDispatcher();
    }

    [TestCleanup]
    public override void TestCleanUp()
    {
      WcfServiceHost.StopService<ICustomerService>();
      WcfServiceHost.StopService<IAddressService>();
      ClientServiceLocator.Instance().CommandDispatcher = _originalCommandDispatcher;
      base.TestCleanUp();
    }
  }
}
