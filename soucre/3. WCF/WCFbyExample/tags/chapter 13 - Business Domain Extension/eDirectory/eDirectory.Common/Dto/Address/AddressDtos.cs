using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.Common.Dto.Address
{
    /// <summary>
    /// version 0.13 - Chapter XIII - Business Domain Extension
    /// </summary>
    public class AddressDtos
        :DtoBase
    {
        public IList<AddressDto> Addresses { get; set; }
    }

}
