using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.Common.Dto.Customer
{
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    public class CustomerDtos
        :DtoBase
    {
        public IList<CustomerDto> Customers { get; set; }
    }
}
