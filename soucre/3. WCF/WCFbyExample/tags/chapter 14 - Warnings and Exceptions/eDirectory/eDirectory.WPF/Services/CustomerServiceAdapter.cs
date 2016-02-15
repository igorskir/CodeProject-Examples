using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;
using eDirectory.Common.Dto.Customer;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.7 Chapter VII: Contract Locator
    /// </remarks>
    class CustomerServiceAdapter
        :ServiceAdapterBase<ICustomerService>, ICustomerService
    {

        #region Constructor

        public CustomerServiceAdapter(ICustomerService service)
            : base(service) { }

        #endregion
        #region ICustomerService Members

        public CustomerDto CreateNewCustomer(CustomerDto customer)
        {
            return ExecuteCommand(() => Service.CreateNewCustomer(customer));
        }

        public CustomerDto GetById(long id)
        {
            return ExecuteCommand(() => Service.GetById(id));
        }

        public CustomerDto UpdateCustomer(CustomerDto customer)
        {
            return ExecuteCommand(() => Service.UpdateCustomer(customer));
        }

        public CustomerDtos FindAll()
        {
            return ExecuteCommand(() => Service.FindAll());
        }

        public DtoResponse DeleteCustomer(long id)
        {
            return ExecuteCommand(() => Service.DeleteCustomer(id));
        }

        public DtoResponse DeleteAddress(long customerId, long addressId)
        {
            return ExecuteCommand(() => Service.DeleteAddress(customerId, addressId));
        }

        #endregion
    }
}
