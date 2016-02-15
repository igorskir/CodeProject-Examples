using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.ServiceContract;

namespace eDirectory.Domain.Services
{
    /// <remarks>
    /// version 0.07 Chapter VII:  Contract Locator
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </remarks>
    public class ServerContractLocator
        :IContractLocator
    {
        ICustomerService CustomerServiceInstance;
        IAddressService AddressServiceInstance;

        #region IContractLocator Members

        public ICustomerService CustomerServices
        {
            get 
            {
                if (CustomerServiceInstance != null) return CustomerServiceInstance;
                CustomerServiceInstance = new CustomerService();
                return CustomerServiceInstance;
            }
        }

        public IAddressService AddressServices
        {
            get
            {
                if (AddressServiceInstance != null) return AddressServiceInstance;
                AddressServiceInstance = new AddressService();
                return AddressServiceInstance;
            }
        }

        #endregion
    }
}
