using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Customer;
using eDirectory.Common.ServiceContract;
using eDirectory.WPF.Core;
using eDirectory.WPF.Customer.View;
using eDirectory.WPF.Customer.Model;
using eDirectory.WPF.Services;
using eDirectory.WPF.Util;

namespace eDirectory.WPF.Customer.ViewModel
{
    /// <remarks>
    /// version 0.06 Chapter VI:   MVVM Pattern
    /// version 0.08 Chapter VIII: RelayCommand
    /// version 0.09 Chapter IX:   Notify Property Changed
    /// version 0.13 Chapter XIII: Async Service Commands
    /// </remarks>
    public class CustomerViewModel
        : ViewModelBase
    {
        private readonly CustomerView View;
        
        private readonly ServiceAdapter<ICustomerService> CustomerServiceAdapter;

        public CustomerViewModel()
        {            
            CustomerServiceAdapter = new ServiceAdapter<ICustomerService>();
            Refresh();
            View = new CustomerView { DataContext = this };
            View.ShowDialog();
        }

        #region Properties

        public CustomerModel Model { get; set; }

        #endregion Properties

        #region Commands

        private RelayCommand SaveCommandInstance;

        public RelayCommand SaveCommand
        {
            get
            {
                if (SaveCommandInstance != null) return SaveCommandInstance;
                SaveCommandInstance = new RelayCommand(a => Save());
                return SaveCommandInstance;
            }
        }

        private void Save()
        {
            Model.IsEnabled = false;
            RaisePropertyChanged(() => Model);
            var result = CustomerServiceAdapter.Execute(s => s.CreateNewCustomer(Model.NewCustomerOperation));
            Refresh();
        }


        private RelayCommand RefreshCommandInstance;

        public RelayCommand RefreshCommand
        {
            get
            {
                if (RefreshCommandInstance != null) return RefreshCommandInstance;
                RefreshCommandInstance = new RelayCommand(a => Refresh());
                return RefreshCommandInstance;
            }
        }
        
        private void Refresh()
        {
            //var result = CustomerServiceInstance.FindAll();
            var adapter = new ServiceAdapter<ICustomerService>();
            var result = adapter.Execute(service => service.FindAll());
            Model = new CustomerModel()
                        {
                            NewCustomerOperation = new CustomerDto(),
                            CustomerList = result.Customers,
                            IsEnabled = true
                        };

            RaisePropertyChanged(() => Model);
        }

        #endregion
    }
}
