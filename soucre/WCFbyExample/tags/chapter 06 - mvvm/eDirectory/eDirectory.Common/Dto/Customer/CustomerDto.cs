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
    public class CustomerDto
        :DtoBase
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
    }   
}
