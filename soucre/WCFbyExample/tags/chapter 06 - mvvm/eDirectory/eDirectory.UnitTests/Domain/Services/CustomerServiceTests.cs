using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using eDirectory.Naive.AppServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Domain.Services;
using eDirectory.Common.Dto.Customer;
using eDirectory.Naive.Repository;
using eDirectory.Domain.Entities;
using eDirectory.Naive.TransManager;
using eDirectory.Domain.AppServices;

namespace eDirectory.UnitTests.Domain.Services
{
    [TestClass]
    public class CustomerServiceTests
    {        
        public CustomerService Service { get; set; }
        public CustomerDto CustomerInstance { get; set; }

        [TestInitialize]
        public void CustomerServiceTestsInitialize() 
        {
            GlobalContext.Instance().TransFactory = new TransManagerEntityStoreFactory();
            Container.RequestContext = new RequestContextNaive();
            Service = new CustomerService ();
        }

        [TestMethod]
        public void CreateCustomer()
        {
            var dto = new CustomerDto
                            {
                                FirstName = "Joe",
                                LastName = "Bloggs",
                                Telephone = "9999-8888"
                            };

            CustomerInstance = Service.CreateNewCustomer(dto);
            Assert.IsFalse(CustomerInstance.CustomerId == 0, "Customer Id should have been updated");
            Assert.AreSame(CustomerInstance.FirstName, dto.FirstName, "First Name are different");
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            CreateCustomer();
            var id = CustomerInstance.CustomerId;
            var dto = new CustomerDto
            {
                CustomerId = id,
                FirstName = "Joe",
                LastName = "Bloggs",
                Telephone = "8888-8888"
            };

            CustomerInstance = Service.UpdateCustomer(dto);
            Assert.IsTrue(CustomerInstance.CustomerId == id, "Customer Id should have remainded the same");
            Assert.AreSame(CustomerInstance.Telephone, "8888-8888", "Incorrect telephone after the update");
        }

        [TestMethod]
        public void FindAll()
        {
            CreateCustomer();
            var result = Service.FindAll();
            Assert.IsTrue(result.Customers.Count == 1, "One customer instance was expected");
        }

        [TestMethod]
        public void CheckFindAllNotification()
        {
            var result = Service.FindAll();
            Assert.IsTrue(result.Customers.Count == 0, "No customers were expected");
            Assert.IsTrue(result.Response.HasWarning, "Warning flag is not set");
            Assert.IsTrue(result.Response.BusinessWarnings.Count() == 1, "One warning was only expected");
            Assert.AreSame(result.Response.BusinessWarnings.Single().Message, "No customer instances were found");
            CreateCustomer();
            result = Service.FindAll();
            Assert.IsFalse(result.Response.HasWarning, "Warning flag is set");
        }

    }
}
