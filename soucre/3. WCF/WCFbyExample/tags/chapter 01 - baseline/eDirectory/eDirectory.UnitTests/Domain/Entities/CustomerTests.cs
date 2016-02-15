using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Naive.Repository;
using eDirectory.Common.Dto.Customer;
using eDirectory.Domain.Entities;

namespace eDirectory.UnitTests.Domain.Entities
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomer()
        {
            // need to create an instance of the repository
            var repository = new RepositoryCustomer();
            var dto = new CustomerDto
                                    { 
                                        FirstName = "Joe",
                                        LastName = "Bloggs",
                                        Telephone = "9999-8888"
                                    };

            var customer = Customer.Create(repository, dto);
            Assert.IsFalse(customer.Id == 0, "Customer Id should have been updated");
            Assert.AreSame(customer.FirstName, dto.FirstName, "First Name are different");
        }
    }
}
