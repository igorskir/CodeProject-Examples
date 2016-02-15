using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.WPF.Customer.View;
using eDirectory.WPF.Customer.Model;

namespace eDirectory.WPF.Customer.ViewModel
{
    /// <remarks>
    /// version 0.6 Chapter VI: MVVM Pattern
    /// </remarks>
    public class CustomerViewModel
    {
        private readonly CustomerView View;
        

        public CustomerViewModel()
        {
            View = new CustomerView();
            View.DataContext = this;
            View.ShowDialog();
        }
        #region Properties

        public CustomerModel Model { get; set; }

        #endregion Properties
    }
}
