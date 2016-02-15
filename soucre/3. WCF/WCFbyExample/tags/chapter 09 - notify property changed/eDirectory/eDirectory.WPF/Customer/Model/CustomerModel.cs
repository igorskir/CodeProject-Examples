using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Customer;

namespace eDirectory.WPF.Customer.Model
{
    /// <remarks>
    /// version 0.6 Chapter VI: MVVM Pattern
    /// </remarks> 
    public class CustomerModel
    {
        public CustomerDto NewCustomerOperation { get; set; }
        public IList<CustomerDto> CustomerList { get; set; }
    }
}
