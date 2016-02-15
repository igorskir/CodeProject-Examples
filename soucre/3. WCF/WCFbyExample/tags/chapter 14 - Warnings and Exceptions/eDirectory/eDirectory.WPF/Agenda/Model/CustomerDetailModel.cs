using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Customer;
using eDirectory.WPF.Core;

namespace eDirectory.WPF.Agenda.Model
{
    /// <summary>
    /// version 0.13 Chapter XIII - Business Domain Extension
    /// </summary>
    class CustomerDetailModel
    {
        public CustomerDto Customer { get; set; }
    }
}
