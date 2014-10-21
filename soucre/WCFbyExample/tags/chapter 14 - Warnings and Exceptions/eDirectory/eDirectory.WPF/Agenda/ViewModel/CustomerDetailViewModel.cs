using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Customer;
using eDirectory.Common.ServiceContract;
using eDirectory.WPF.Agenda.Model;
using eDirectory.WPF.Agenda.View;
using eDirectory.WPF.Core;
using eDirectory.WPF.Services;
using eDirectory.WPF.Util;

namespace eDirectory.WPF.Agenda.ViewModel
{
    /// <summary>
    /// version 0.13 Chapter XIII - Business Domain Extension
    /// </summary>
    class CustomerDetailViewModel
        :ViewModelBase
    {
        #region Private References

        private readonly CustomerDetailView View;
        private readonly ICustomerService CustomerServiceInstance;

        #endregion Private References
        #region Constructor

        public CustomerDetailViewModel(CustomerDto operation)
        {
            CustomerServiceInstance = ClientServiceLocator.Instance().ContractLocator.CustomerServices;
            Model = new CustomerDetailModel {Customer = new CustomerDto()};
            if(operation != null) Model.Customer = operation;
            View = new CustomerDetailView {DataContext = this};
        }

        #endregion Constructor
        #region Public Property and Methods

        private bool IsInDialogMode;
        public bool? ShowDialog()
        {
            if (IsInDialogMode) return null;
            IsInDialogMode = true;
            return View.ShowDialog();
        }

        public CustomerDetailModel Model { get; set; }

        #endregion Public Property and Methods
        #region Commands

        private RelayCommand SaveCommandInstance;
        public RelayCommand SaveCommand
        {
            get
            {
                if (SaveCommandInstance != null) return SaveCommandInstance;
                SaveCommandInstance = new RelayCommand(a => Save(), 
                                                       p => Model.Customer.IsValid());

                return SaveCommandInstance;
            }
        }

        private void Save()
        {
            if (Model.Customer.Id == 0)
            {
                Model.Customer = CustomerServiceInstance.CreateNewCustomer(Model.Customer);
            }
            else
            {
                Model.Customer = CustomerServiceInstance.UpdateCustomer(Model.Customer);
            }

            View.DialogResult = true;
            View.Close();
        }

        private RelayCommand CancelCommandInstance;
        public RelayCommand CancelCommand
        {
            get
            {
                if (CancelCommandInstance != null) return CancelCommandInstance;
                CancelCommandInstance = new RelayCommand(a =>
                                                             {
                                                                 View.DialogResult = false;
                                                                 View.Close();
                                                             });

                return CancelCommandInstance;
            }
        }

        #endregion Commands
    }
}
