using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.Common.Dto.Address
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    public class AddressDto
        :DtoBase
    {
        public long CustomerId { get; set; }
        public long Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
