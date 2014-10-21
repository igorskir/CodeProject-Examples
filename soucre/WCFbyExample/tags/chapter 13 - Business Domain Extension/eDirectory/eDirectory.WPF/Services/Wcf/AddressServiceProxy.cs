using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services.Wcf
{
    /// <summary>
    /// version 0.13 Capter XIII - Business Domain Extension
    /// </summary>
    public class AddressServiceProxy
        : WcfAdapterBase<IAddressService>, IAddressService
    {
        #region Implementation of IAddressService

        public AddressDto GetById(long id)
        {
            return ExecuteCommand(proxy => proxy.GetById(id));
        }

        public AddressDto UpdateAddress(AddressDto address)
        {
            return ExecuteCommand(proxy => proxy.UpdateAddress(address));
        }

        public AddressDto CreateNewAddress(AddressDto address)
        {
            return ExecuteCommand(proxy => proxy.CreateNewAddress(address));
        }

        #endregion
    }
}
