using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services.Wcf
{
    /// <summary>
    /// version 0.12 Chapter XII  - WCF Implementation
    /// version 0.13 Chapter XIII - Business Domain Extension - Address Service
    /// </summary>
    public class WcfContractLocator
        :IContractLocator
    {
        private ICustomerService CustomerServiceProxyInstance;
        private IAddressService AddressServiceProxyInstance;

        public ICustomerService CustomerServices
        {
            get 
            {
                if (CustomerServiceProxyInstance != null) return CustomerServiceProxyInstance;
                CustomerServiceProxyInstance = new CustomerServiceProxy();
                return CustomerServiceProxyInstance;
            }
        }

        public IAddressService AddressServices
        {
            get
            {
                if (AddressServiceProxyInstance != null) return AddressServiceProxyInstance;
                AddressServiceProxyInstance = new AddressServiceProxy();
                return AddressServiceProxyInstance;
            }
        }
    }
}
