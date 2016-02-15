using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Customer;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services.Wcf
{
    public class CustomerServiceProxy
        :WcfAdapterBase<ICustomerService>, ICustomerService
    {
        #region Implementation of ICustomerService

        public CustomerDto CreateNewCustomer(CustomerDto customer)
        {
            return ExecuteCommand(proxy => proxy.CreateNewCustomer(customer));
        }

        public CustomerDto GetById(long id)
        {
            return ExecuteCommand(proxy => proxy.GetById(id));
        }

        public CustomerDto UpdateCustomer(CustomerDto customer)
        {
            return ExecuteCommand(proxy => proxy.UpdateCustomer(customer));
        }

        public CustomerDtos FindAll()
        {
            return ExecuteCommand(proxy => proxy.FindAll());
        }

        public DtoResponse DeleteCustomer(long id)
        {
            return ExecuteCommand(proxy => proxy.DeleteCustomer(id));
        }

        public DtoResponse DeleteAddress(long customerId, long addressId)
        {
            return ExecuteCommand(proxy => proxy.DeleteAddress(customerId, addressId));
        }

        #endregion
    }
}
