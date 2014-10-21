using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Domain.Services;
using eDirectory.Common.Dto.Customer;
using eDirectory.Naive.Repository;
using eDirectory.Domain.Entities;

namespace eDirectory.UnitTests.Domain.Services
{
    [TestClass]
    public class CustomerServiceTests
    {        
        public CustomerService Service { get; set; }
        public CustomerDto CustomerInstance { get; set; }

        [TestInitialize()]
        public void CustomerServiceTestsInitialize() 
        {
            Service = new CustomerService { Repository = new RepositoryLocatorEntityStore() };
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

    }
}
