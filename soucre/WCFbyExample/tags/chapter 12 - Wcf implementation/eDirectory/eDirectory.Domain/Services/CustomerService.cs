using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using eDirectory.Common.ServiceContract;
using eDirectory.Common.Dto.Customer;
using eDirectory.Domain.AppServices.WcfRequestContext;
using eDirectory.Domain.Entities;
using eDirectory.Domain.Repository;
using eDirectory.Domain.AppServices;
using eDirectory.Common.Message;

namespace eDirectory.Domain.Services
{
    /// <remarks>
    /// version 0.04 Chapter IV:   Transaction Manager
    /// version 0.07 Context Re-Factor
    /// version 0.13 Chapter XIII: Request Context
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [InstanceCreation]
    public class CustomerService
        :ServiceBase, ICustomerService
    {
        
        #region ICustomerService Members

        public CustomerDto CreateNewCustomer(CustomerDto dto)
        {
            return ExecuteCommand(locator => CreateNewCustomerCommand(locator, dto));
        }

        private CustomerDto CreateNewCustomerCommand(IRepositoryLocator locator, CustomerDto dto)
        {
            var customer = Customer.Create(locator, dto);
            //Thread.Sleep(10000);
            return Customer_to_Dto(customer);
        }

        public CustomerDto GetById(long id)
        {
            return ExecuteCommand(locator => Customer_to_Dto(locator.GetById<Customer>(id)));
        }

        public CustomerDto UpdateCustomer(CustomerDto dto)
        {
            return ExecuteCommand(locator => UpdateCustomerCommand(locator, dto));
        }

        private CustomerDto UpdateCustomerCommand(IRepositoryLocator locator, CustomerDto dto)
        {
            var instance = locator.GetById<Customer>(dto.CustomerId);
            instance.Update(locator, dto);            
            return Customer_to_Dto(instance);
        }

        public CustomerDtos FindAll()
        {
            return ExecuteCommand(FindAllCommand);
        }

        private CustomerDtos FindAllCommand(IRepositoryLocator locator)
        {
            var result = new CustomerDtos { Customers = new List<CustomerDto>() };
            locator.FindAll<Customer>().ToList()
                                .ForEach(c => result.Customers.Add(Customer_to_Dto(c)));

            if (result.Customers.Count() == 0) 
            { 
                Container.RequestContext.Notifier.AddWarning(
                    BusinessWarningEnum.Default, "No customer instances were found"); 
            }
            return result;
        }

        #endregion

        private CustomerDto Customer_to_Dto(Customer customer)
        {
            return new CustomerDto
            {
                CustomerId = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Telephone = customer.Telephone
            };
        }
    }
}
