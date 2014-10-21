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
    /// version 0.2 Chapter II: Repository
    /// </remarks>
    public class CustomerService
        :ICustomerService
    {
        public IRepositoryLocator Repository { get; set; }
        
        #region ICustomerService Members

        public Common.Dto.Customer.CustomerDto CreateNewCustomer(CustomerDto dto)
        {            
            var customer = Customer.Create(Repository, dto);
            return new CustomerDto
                            {
                                CustomerId = customer.Id,
                                FirstName = customer.FirstName,
                                LastName = customer.LastName,
                                Telephone = customer.Telephone
                            };
        }

        public Common.Dto.Customer.CustomerDto GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Common.Dto.Customer.CustomerDto UpdateCustomer(CustomerDto customer)
        {
            throw new NotImplementedException();
        }

        public List<Common.Dto.Customer.CustomerDto> FindAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
