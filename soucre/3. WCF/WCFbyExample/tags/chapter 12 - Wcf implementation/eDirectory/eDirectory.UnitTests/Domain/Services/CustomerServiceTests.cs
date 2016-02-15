using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Common.Dto.Customer;
using eDirectory.WPF.Services;
using eDirectory.Common.ServiceContract;

namespace eDirectory.UnitTests.Domain.Services
{
    
    [TestClass]
    public class CustomerServiceTests
        :eDirectoryTestBase
    {        
        public ServiceAdapter<ICustomerService> ServiceAdapter;
        public CustomerDto CustomerInstance { get; set; }

        [TestInitialize]
        public override void TestsInitialize()
        {
            base.TestsInitialize();
            ServiceAdapter = new ServiceAdapter<ICustomerService>();
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

            CustomerInstance = ServiceAdapter.Execute(s => s.CreateNewCustomer(dto));
            Assert.IsFalse(CustomerInstance.CustomerId == 0, "Customer Id should have been updated");
            Assert.AreEqual(CustomerInstance.FirstName, dto.FirstName, "First Name are different");
        }

        [TestMethod]
        public virtual void UpdateCustomer()
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

            CustomerInstance = ServiceAdapter.Execute(s => s.UpdateCustomer(dto));
            Assert.IsTrue(CustomerInstance.CustomerId == id, "Customer Id should have remainded the same");
            Assert.AreEqual(CustomerInstance.Telephone, "8888-8888", "Incorrect telephone after the update");
        }

        [TestMethod]
        public virtual void FindAll()
        {
            CreateCustomer();
            var result = ServiceAdapter.Execute(s => s.FindAll());
            Assert.IsTrue(result.Customers.Count == 1, "One customer instance was expected");
        }

        [TestMethod]
        public virtual void CheckFindAllNotification()
        {
            var result = ServiceAdapter.Execute(s => s.FindAll());
            Assert.IsTrue(result.Customers.Count == 0, "No customers were expected");
            Assert.IsTrue(result.Response.HasWarning, "Warning flag is not set");
            Assert.IsTrue(result.Response.BusinessWarnings.Count() == 1, "One warning was only expected");
            Assert.AreEqual(result.Response.BusinessWarnings.Single().Message, "No customer instances were found");
            CreateCustomer();
            result = ServiceAdapter.Execute(s => s.FindAll());
            Assert.IsFalse(result.Response.HasWarning, "Warning flag is set");
        }

    }
}
