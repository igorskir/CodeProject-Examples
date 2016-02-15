using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.ServiceContract
{
    /// <remarks>
    /// version 0.07 Chapter VII:  Contract Locator
    /// version 0.13 Chapter XIII: Business Domain Extension - New Address Service
    /// </remarks>
    public interface IContractLocator
    {
        ICustomerService CustomerServices { get; }
        IAddressService AddressServices { get; }
    }
}
