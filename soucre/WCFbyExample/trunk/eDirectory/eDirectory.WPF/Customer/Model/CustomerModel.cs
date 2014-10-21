using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Customer;

namespace eDirectory.WPF.Customer.Model
{
    /// <remarks>
    /// version 0.06 Chapter VI:   MVVM Pattern
    /// version 0.13 Chapter XIII: Async service methods
    /// </remarks> 
    public class CustomerModel
    {
        public CustomerDto NewCustomerOperation { get; set; }
        public IList<CustomerDto> CustomerList { get; set; }

        public bool IsEnabled { get; set; }
    }
}
