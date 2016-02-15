using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.ServiceContract;
using eDirectory.Common.Dto.Customer;
using eDirectory.Domain.Entities;
using eDirectory.Domain.Repository;

namespace eDirectory.Domain.Services
{
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    public class CustomerService
        :ICustomerService
    {
        public IRepositoryLocator Repository { get; set; }
        
        #region ICustomerService Members

        public CustomerDto CreateNewCustomer(CustomerDto dto)
        {            
            var customer = Customer.Create(Repository, dto);
            return Customer_to_Dto(customer);
        }

        public CustomerDto GetById(long id)
        {
            return Customer_to_Dto(Repository.GetById<Customer>(id));
        }

        public CustomerDto UpdateCustomer(CustomerDto dto)
        {
            var instance = Repository.GetById<Customer>(dto.CustomerId);
            instance.Update(Repository, dto);
            return Customer_to_Dto(instance);
        }

        public CustomerDtos FindAll()
        {
            var customers = Repository.FindAll<Customer>();
            var result = new CustomerDtos {Customers = new List<CustomerDto>()};
            if (customers.Count() == 0) return result;
            customers.ToList().ForEach(c => result.Customers.Add(Customer_to_Dto(c)));
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
