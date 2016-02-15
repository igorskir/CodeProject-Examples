using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.07 Chapter VII: Contract Locator
    /// version 0.10 Chapter X: Dependency Injection
    /// version 0.13 Chapter XIII: Business Domain Extension - Address Interface
    /// </remarks>
    public class ClientContractLocator
        :IContractLocator
    {
        private ICustomerService CustomerServiceInstance;
        private IAddressService AddressServiceInstance;

        public IContractLocator NextAdapterLocator { get; private set; }

        #region IContractLocator Members

        public ICustomerService CustomerServices
        {
            get 
            {
                if (CustomerServiceInstance != null) return CustomerServiceInstance;
                CustomerServiceInstance = new CustomerServiceAdapter(NextAdapterLocator.CustomerServices);
                return CustomerServiceInstance;
            }
        }

        public IAddressService AddressServices
        {
            get
            {
                if (AddressServiceInstance != null) return AddressServiceInstance;
                AddressServiceInstance = new AddressServiceAdapter(NextAdapterLocator.AddressServices);
                return AddressServiceInstance;
            }
        }

        #endregion
    }
}
