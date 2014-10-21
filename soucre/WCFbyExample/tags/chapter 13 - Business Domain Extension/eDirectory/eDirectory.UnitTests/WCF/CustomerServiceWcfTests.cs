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
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    [TestClass]
    public class CustomerServiceWcfTests
        : CustomerServiceTests
    {
        [TestInitialize]
        public override void TestsInitialize()
        {
            base.TestsInitialize();
            WcfServiceHost.StartService<CustomerService, ICustomerService>();
            WcfServiceHost.StartService<AddressService, IAddressService>();
        }

        [TestCleanup]
        public override void TestCleanUp()
        {
            WcfServiceHost.StopService<ICustomerService>();
            WcfServiceHost.StopService<IAddressService>();
            base.TestCleanUp();
        }

        [TestMethod]
        public override void CreateCustomer()
        {
            ExecuteWithCustomerService(base.CreateCustomer);
        }

        [TestMethod]
        public override void FindAll()
        {
            ExecuteWithCustomerService(base.FindAll);
        }

        [TestMethod]
        public override void CheckFindAllNotification()
        {
            ExecuteWithCustomerService(base.CheckFindAllNotification);
        }

        [TestMethod]
        public override void UpdateCustomer()
        {
            ExecuteWithCustomerService(base.UpdateCustomer);
        }

        [TestMethod]
        public override void CreateAddress()
        {
            ExecuteWithAddressService(base.CreateAddress);
        }

        [TestMethod]
        public override void GetAddressById()
        {
            ExecuteWithAddressService(base.GetAddressById);
        }

        [TestMethod]
        public override void UpdateAddress()
        {
            ExecuteWithAddressService(base.UpdateAddress);
        }

        private void ExecuteWithCustomerService(Action action)
        {
            WcfServiceHost.InvokeService<ICustomerService>
                (
                    service =>
                        {
                            this.CustomerService = service;
                            action.Invoke();
                        }
                );
        }

        private void ExecuteWithAddressService(Action action)
        {
            WcfServiceHost.InvokeService<IAddressService>
                (
                    service =>
                    {
                        this.AddressService = service;
                        action.Invoke();
                    }
                );
        }

    }
}
