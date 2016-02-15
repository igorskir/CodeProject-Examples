using System.Collections.Generic;
using AutoMapper;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Dto.Customer;

namespace eDirectory.Domain.Entities
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    public class AutoMapperConfigurator
    {
        public static void Install()
        {
            Mapper.CreateMap<Customer, CustomerDto>()
                .ForMember(d => d.Addresses, 
                           opt => opt.MapFrom(src => Mapper.Map<ICollection<Address>, List<AddressDto>>(src.Addresses())));

            Mapper.CreateMap<Address, AddressDto>()
                .ForMember(d => d.CustomerId,
                           opt => opt.MapFrom(src => src.Customer.Id));
        }
    }
}
