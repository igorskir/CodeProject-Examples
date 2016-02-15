using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Message;

namespace eDirectory.Common.Dto.Customer
{
    /// <remarks>
    /// version 0.03 Chapter III:  The Response
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </remarks>
    public class CustomerDto
        :DtoBase
    {
        public CustomerDto()
        {
            Addresses = new List<AddressDto>();
        }
        
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }

        public List<AddressDto> Addresses { get; set; }
    }   
}
