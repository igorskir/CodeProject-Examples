using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Domain.Services;
using eDirectory.Common.Dto.Customer;
using eDirectory.Naive.Repository;

namespace eDirectory.UnitTests.Domain.Services
{
    [TestClass]
    public class CustomerServiceTests
    {
        [TestMethod]
        public void CreateCustomer()
        {
            // need to create an instance of the repository
            var service = new CustomerService { Repository = new RepositoryLocatorEntityStore() };
            var dto = new CustomerDto
                            {
                                FirstName = "Joe",
                                LastName = "Bloggs",
                                Telephone = "9999-8888"
                            };

            var customer = service.CreateNewCustomer(dto);
            Assert.IsFalse(customer.CustomerId == 0, "Customer Id should have been updated");
            Assert.AreSame(customer.FirstName, dto.FirstName, "First Name are different");
        }
    }
}
