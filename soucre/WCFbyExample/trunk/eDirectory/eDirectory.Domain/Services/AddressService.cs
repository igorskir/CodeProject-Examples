using System.ServiceModel;
using AutoMapper;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;
using eDirectory.Domain.AppServices;
using eDirectory.Domain.AppServices.WcfRequestContext;
using eDirectory.Domain.Entities;
using eDirectory.Domain.Repository;

namespace eDirectory.Domain.Services
{
    /// <summary>
    /// version 0.13 Chapter XIII - Business Domain Extension
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [InstanceCreation]
    public class AddressService
        :ServiceBase, IAddressService
    {

        private readonly IBusinessNotifier BusinessNotifier;

        public AddressService()
        {
            BusinessNotifier = Container.RequestContext.Notifier;
        }

        #region Implementation of IAddressService

        public AddressDto GetById(long id)
        {
            return ExecuteCommand(locator => AddressToAddressDto(locator.GetById<Address>(id)));
        }

        public AddressDto UpdateAddress(AddressDto address)
        {
            return ExecuteCommand(locator => UpdateAddressCommand(locator, address));
        }

        private static AddressDto UpdateAddressCommand(IRepositoryLocator locator, AddressDto dto)
        {
            var address = locator.GetById<Address>(dto.Id);
            address.Update(locator, dto);
            return AddressToAddressDto(address);
        }

        public AddressDto CreateNewAddress(AddressDto address)
        {
            return ExecuteCommand(locator => CreateNewAddressCommand(locator, address));
        }

        private static AddressDto CreateNewAddressCommand(IRepositoryLocator locator, AddressDto dto)
        {
            var customer = locator.GetById<Customer>(dto.CustomerId);
            return AddressToAddressDto(customer.AddAddress(locator, dto));
        }

        private DtoResponse DeleteAddressCommand(IRepositoryLocator locator, long id)
        {
            var address = locator.GetById<Address>(id);
            BusinessNotifier.AddWarning(BusinessWarningEnum.Default,
                                        string.Format("Address with id:{0} was deleted",
                                        address.Id));

            locator.Remove(address);
            return new DtoResponse();
        }

        #endregion
        #region Private Methods

        private static AddressDto AddressToAddressDto(Address address)
        {
            var dto = Mapper.Map<Address, AddressDto>(address);
            return dto;
        }

        #endregion

    }
}
