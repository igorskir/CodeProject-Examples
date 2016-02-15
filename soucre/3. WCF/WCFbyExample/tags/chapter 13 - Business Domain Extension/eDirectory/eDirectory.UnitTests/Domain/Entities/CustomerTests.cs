using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using eDirectory.Common.Dto.Address;
using eDirectory.Domain.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Naive.Repository;
using eDirectory.Common.Dto.Customer;
using eDirectory.Domain.Entities;

namespace eDirectory.UnitTests.Domain.Entities
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    [TestClass]
    public class CustomerTests
    {
        private IRepositoryLocator RepositoryLocator;
        private Customer CustomerInstance;

        [TestMethod]
        public void CreateCustomer()
        {
            // need to create an instance of the repository locator
            RepositoryLocator = new RepositoryLocatorInMemory();
            var dto = new CustomerDto
                                    { 
                                        FirstName = "Joe",
                                        LastName = "Bloggs",
                                        Telephone = "9999-8888"
                                    };

            CustomerInstance = Customer.Create(RepositoryLocator, dto);
            Assert.IsFalse(CustomerInstance.Id == 0, "Customer Id should have been updated");
            Assert.AreSame(CustomerInstance.FirstName, dto.FirstName, "First Name are different");
        }

        [TestMethod]
        public void AddAddress()
        {
            CreateCustomer();
            var dto = new AddressDto
                          {
                              CustomerId = CustomerInstance.Id,
                              Street = "101 Thames Street",
                              City = "Dublin",
                              PostCode = "16",
                              Country = "Ireland"
                          };

            var address = CustomerInstance.AddAddress(RepositoryLocator, dto);
            Assert.AreEqual(1, CustomerInstance.Addresses().Count, "Incorrect number of addresses were found at the customer");
        }
    }
}
