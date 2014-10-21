using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Dto.Customer;

namespace eDirectory.WPF.Agenda.Model
{
    /// <summary>
    /// version 0.13 Chapter XIII - Business Domain Extension
    /// </summary>
    class AgendaModel
    {
        public IList<CustomerDto> CustomerList { get; set; }
        public CustomerDto SelectedCustomer { get; set; }
        public AddressDto SelectedAddress { get; set; }
    }
}
