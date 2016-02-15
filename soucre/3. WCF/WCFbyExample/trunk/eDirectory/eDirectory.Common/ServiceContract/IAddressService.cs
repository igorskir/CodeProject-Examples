using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using eDirectory.Common.Dto.Address;

namespace eDirectory.Common.ServiceContract
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    [ServiceContract(Namespace = "http://wcfbyexample/addressservices/")]
    public interface IAddressService
        :IContract
    {
        [OperationContract]
        AddressDto GetById(long id);

        [OperationContract]
        AddressDto UpdateAddress(AddressDto address);

        [OperationContract]
        AddressDto CreateNewAddress(AddressDto address);
    }
}
