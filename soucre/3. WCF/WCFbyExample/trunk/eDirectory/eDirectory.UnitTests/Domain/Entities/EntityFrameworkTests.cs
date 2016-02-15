using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Dto.Customer;
using eDirectory.Common.Message;
using eDirectory.Domain.Entities;
using eDirectory.Domain.Mappings;
using eDirectory.Domain.Repository;
using eDirectory.EF.TransManager;

namespace eDirectory.UnitTests.Domain.Entities
{
  [TestClass]
  public class EntityFrameworkTests
  {
    [TestMethod]
    public void FlushUpdatesEntityId()
    {
      var factory = new TransManagerFactoryEF(new ModelCreator());
      DtoResponse response;
      string msg;
      using (var manager = factory.CreateManager())
      {
        response = manager.ExecuteCommand(DeleteTestCustomer);
        msg = response.Response.BusinessException != null
                ? response.Response.BusinessException.Message
                : "UnKnown error when deleting records";

        Assert.IsFalse(response.Response.HasException, msg);
      }
      using (var manager = factory.CreateManager())
      {
        response = manager.ExecuteCommand(CreateCustomer);
        msg = response.Response.BusinessException != null
                ? response.Response.BusinessException.Message
                : "UnKnown error when creating customer";

        Assert.IsFalse(response.Response.HasException, msg);
      }
      using (var manager = factory.CreateManager())
      {
        response = manager.ExecuteCommand(CheckOnlyOneCustomer);
        msg = response.Response.BusinessException != null
                ? response.Response.BusinessException.Message
                : "UnKnown error when checking customer";

        Assert.IsFalse(response.Response.HasException, msg);
      }
    }

    private DtoResponse CheckOnlyOneCustomer(IRepositoryLocator locator)
    {
      var customers = locator.FindAll<Customer>().Where(c => c.FirstName == "TestName").ToList();
      Assert.AreEqual(1, customers.Count);
      return new DtoResponse();
    }

    private DtoResponse CreateCustomer(IRepositoryLocator locator)
    {
      var customer = Customer.Create(locator, new CustomerDto
        {
          FirstName = "TestName",
          LastName = "TestLastName",
          Telephone = "999-0000"
        });

      customer.AddAddress(locator, new AddressDto
        {
          City = "TestCity",
          Country = "TestCountry",
          PostCode = "22",
          Street = "TestStreet"
        });

      Assert.AreEqual(0, customer.Id);
      locator.FlushModifications();
      Assert.AreNotEqual(0, customer.Id);
      return new DtoResponse();
    }

    private DtoResponse DeleteTestCustomer(IRepositoryLocator locator)
    {
      var customers = locator.FindAll<Customer>().Where(c => c.FirstName == "TestName").ToList();
      foreach (var customer in customers)
      {
        foreach (var address in customer.Addresses())
        {
          customer.DeleteAddress(locator, address.Id);
        }
        locator.Remove(customer);
      }
      return new DtoResponse();
    }
  }
}
