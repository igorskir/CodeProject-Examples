﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using eDirectory.Common.DependencyInjection;
using eDirectory.Common.Dto.Address;
using eDirectory.Naive.AppServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Domain.Services;
using eDirectory.Common.Dto.Customer;
using eDirectory.Naive.Repository;
using eDirectory.Domain.Entities;
using eDirectory.Naive.TransManager;
using eDirectory.Domain.AppServices;
using eDirectory.WPF.Services;
using eDirectory.Common.ServiceContract;

namespace eDirectory.UnitTests.Domain.Services
{

    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    [TestClass]
    public class CustomerServiceTests
        : eDirectoryTestBase
    {
        public ICustomerService CustomerService { get; set; }
        public IAddressService AddressService { get; set; }
        public CustomerDto CustomerInstance { get; set; }
        public AddressDto AddressInstance { get; set; }

        [TestInitialize]
        public override void TestsInitialize()
        {
            base.TestsInitialize();
            CustomerService = ClientServiceLocator.Instance().ContractLocator.CustomerServices;
            AddressService = ClientServiceLocator.Instance().ContractLocator.AddressServices;
        }

        [TestMethod]
        public virtual void CreateCustomer()
        {
            var dto = new CustomerDto
                            {
                                FirstName = "Joe",
                                LastName = "Bloggs",
                                Telephone = "9999-8888"
                            };

            CustomerInstance = CustomerService.CreateNewCustomer(dto);
            Assert.IsFalse(CustomerInstance.Id == 0, "Customer Id should have been updated");
            Assert.AreEqual(CustomerInstance.FirstName, dto.FirstName, "First Name are different");
        }

        [TestMethod]
        public virtual void UpdateCustomer()
        {
            CreateCustomer();
            var id = CustomerInstance.Id;
            var dto = new CustomerDto
            {
                Id = id,
                FirstName = "Joe",
                LastName = "Bloggs",
                Telephone = "8888-8888"
            };

            CustomerInstance = CustomerService.UpdateCustomer(dto);
            Assert.IsTrue(CustomerInstance.Id == id, "Customer Id should have remainded the same");
            Assert.AreEqual(CustomerInstance.Telephone, "8888-8888", "Incorrect telephone after the update");
        }

        [TestMethod]
        public virtual void FindAll()
        {
            CreateCustomer();
            var result = CustomerService.FindAll();
            Assert.IsTrue(result.Customers.Count == 1, "One customer instance was expected");
        }

        [TestMethod]
        public virtual void CheckFindAllNotification()
        {
            var result = CustomerService.FindAll();
            Assert.IsTrue(result.Customers.Count == 0, "No customers were expected");
            Assert.IsTrue(result.Response.HasWarning, "Warning flag is not set");
            Assert.IsTrue(result.Response.BusinessWarnings.Count() == 1, "One warning was only expected");
            Assert.AreEqual(result.Response.BusinessWarnings.Single().Message, "No customer instances were found");
            CreateCustomer();
            result = CustomerService.FindAll();
            Assert.IsFalse(result.Response.HasWarning, "Warning flag is set");
        }

        [TestMethod]
        public virtual void CreateAddress()
        {
            CreateCustomer();
            AddressInstance = AddressService.CreateNewAddress(new AddressDto
                                                                  {
                                                                      CustomerId = CustomerInstance.Id,
                                                                      Street = "23 Thames Street",
                                                                      City = "London",
                                                                      PostCode = "16A",
                                                                      Country = "England"
                                                                  });

            Assert.AreEqual(CustomerInstance.Id, AddressInstance.CustomerId, "incorrect customer id was found");
            Assert.AreNotEqual(0, AddressInstance.Id, "incorrect address id was returned");

            var customer = CustomerService.GetById(CustomerInstance.Id);
            Assert.AreEqual(1, customer.Addresses.Count, "One address was expected");
        }

        [TestMethod]
        public virtual void UpdateAddress()
        {
            CreateAddress();
            var dto = new AddressDto
                          {
                              Id = AddressInstance.Id,
                              CustomerId = AddressInstance.CustomerId,
                              Street = "55 River Road",
                              City = "Denver",
                              PostCode = "6099",
                              Country = "USA"
                          };

            var result = AddressService.UpdateAddress(dto);
            Assert.AreEqual(AddressInstance.CustomerId, result.CustomerId, "incorrect customer id was found");
            Assert.AreEqual(AddressInstance.Id, result.Id, "incorrect address id was found");
            Assert.AreEqual("USA", result.Country, "incorrect country was returned" );            
        }

        [TestMethod]
        public virtual void GetAddressById()
        {
            CreateAddress();
            var id = AddressInstance.Id;

            var result = AddressService.GetById(id);

            Assert.AreEqual(AddressInstance.CustomerId, result.CustomerId, "incorrect customer id was found");
            Assert.AreEqual(AddressInstance.Id, result.Id, "incorrect address id was found");
            Assert.AreEqual("London", result.City, "incorrect city was returned");  

        }
    }
}
