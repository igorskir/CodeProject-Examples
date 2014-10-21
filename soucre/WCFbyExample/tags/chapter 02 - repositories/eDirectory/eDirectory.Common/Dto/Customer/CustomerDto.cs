using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.Dto.Customer
{

    /// <remarks>
    /// version 0.1 Chapter: Introduction
    /// </remarks>
    public class CustomerDto
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
    }   
}
