using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AutoMapper;
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
    /// version 0.07               Context Re-Factor
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [InstanceCreation]
    public class CustomerService
        :ServiceBase, ICustomerService
    {
        private readonly IBusinessNotifier BusinessNotifier;

        public CustomerService()
        {
            BusinessNotifier = Container.RequestContext.Notifier;
        }

        #region ICustomerService Members

        public CustomerDto CreateNewCustomer(CustomerDto dto)
        {
            return ExecuteCommand(locator => CreateNewCustomerCommand(locator, dto));
        }

        private CustomerDto CreateNewCustomerCommand(IRepositoryLocator locator, CustomerDto dto)
        {
            var customer = Customer.Create(locator, dto);
            locator.FlushModifications();
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
            var instance = locator.GetById<Customer>(dto.Id);
            instance.Update(locator, dto);            
            return Customer_to_Dto(instance);
        }

        public CustomerDtos FindAll()
        {
            return ExecuteCommand(FindAllCommand);
        }

        public DtoResponse DeleteCustomer(long id)
        {
            return ExecuteCommand(locator => DeleteCustomerCommand(locator, id));
        }

        public DtoResponse DeleteAddress(long customerId, long addressId)
        {
            return ExecuteCommand(locator => DeleteAddressCommand(locator, customerId, addressId));
        }

        private DtoResponse DeleteAddressCommand(IRepositoryLocator locator, long customerId, long addressId)
        {
            var customer = locator.GetById<Customer>(customerId);
            customer.DeleteAddress(locator, addressId);
            return new DtoResponse();

        }

        private DtoResponse DeleteCustomerCommand(IRepositoryLocator locator, long id)
        {
            var customer = locator.GetById<Customer>(id);            
            BusinessNotifier.AddWarning(BusinessWarningEnum.Default,
                                        string.Format("Customer {0} {1} [id:{2}] was deleted",
                                                      customer.FirstName,
                                                      customer.LastName,
                                                      customer.Id));

            locator.Remove(customer);
            return new DtoResponse();
        }

        private CustomerDtos FindAllCommand(IRepositoryLocator locator)
        {
            var result = new CustomerDtos { Customers = new List<CustomerDto>() };
            var customers = locator.FindAll<Customer>().ToList();
            result.Customers = Mapper.Map<List<Customer>, List<CustomerDto>>(customers);
            if (result.Customers.Count() == 0) 
            { 
                BusinessNotifier.AddWarning(BusinessWarningEnum.Default, "No customer instances were found"); 
            }
            return result;
        }

        #endregion

        private CustomerDto Customer_to_Dto(Customer customer)
        {
            return Mapper.Map<Customer, CustomerDto>(customer);          
        }
    }
}
