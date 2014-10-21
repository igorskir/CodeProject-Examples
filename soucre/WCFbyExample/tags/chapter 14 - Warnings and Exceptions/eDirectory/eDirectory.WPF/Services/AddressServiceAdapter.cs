using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    /// <summary>
    /// version 0.13 Capter XIII - Business Domain Extension
    /// </summary>
    public class AddressServiceAdapter
        : ServiceAdapterBase<IAddressService>, IAddressService
    {
        public AddressServiceAdapter(IAddressService service) 
            : base(service) { }

        #region Implementation of IAddressService

        public AddressDto GetById(long id)
        {
            return ExecuteCommand(() => Service.GetById(id));
        }

        public AddressDto UpdateAddress(AddressDto address)
        {
            return ExecuteCommand(() => Service.UpdateAddress(address));
        }

        public AddressDto CreateNewAddress(AddressDto address)
        {
            return ExecuteCommand(() => Service.CreateNewAddress(address));
        }

        #endregion
    }
}
