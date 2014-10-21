using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Dto.Customer;
using eDirectory.Common.ServiceContract;
using eDirectory.WPF.Agenda.Model;
using eDirectory.WPF.Agenda.View;
using eDirectory.WPF.Core;
using eDirectory.WPF.Customer.Model;
using eDirectory.WPF.Services;
using eDirectory.WPF.Util;

namespace eDirectory.WPF.Agenda.ViewModel
{
    /// <summary>
    /// version 0.13 Chapter XIII - Business Domain Extension
    /// </summary>
    class AgendaViewModel
        :ViewModelBase
    {
        private readonly AgendaView View;
        private readonly ICustomerService CustomerServiceInstance;       

        public AgendaViewModel()
        {
            CustomerServiceInstance = ClientServiceLocator.Instance().ContractLocator.CustomerServices;
            Refresh();
            View = new AgendaView {DataContext = this};
            View.ShowDialog();
        }

        #region Properties

        public AgendaModel Model { get; set; }

        #endregion Properties
        #region Commands

        private RelayCommand DeleteCustomerCommandInstance;
        public RelayCommand DeleteCustomerCommand
        {
            get
            {
                if (DeleteCustomerCommandInstance != null) return DeleteCustomerCommandInstance;
                DeleteCustomerCommandInstance = new RelayCommand(a => DeleteCustomer(Model.SelectedCustomer.Id), 
                                                                 p => Model.SelectedCustomer != null && Model.SelectedCustomer.Addresses.Count == 0);

                return DeleteCustomerCommandInstance;
            }
        }

        public void DeleteCustomer(long id)
        {
            CustomerServiceInstance.DeleteCustomer(id);
            Refresh();
        }

        private RelayCommand DeleteAddressCommandInstance;
        public RelayCommand DeleteAddressCommand
        {
            get
            {
                if (DeleteAddressCommandInstance != null) return DeleteAddressCommandInstance;
                DeleteAddressCommandInstance = new RelayCommand(a => DeleteAddress(Model.SelectedCustomer.Id, Model.SelectedAddress.Id),
                                                                p => Model.SelectedAddress != null);

                return DeleteAddressCommandInstance;
            }
        }

        public void DeleteAddress(long customerId, long addressId)
        {
            CustomerServiceInstance.DeleteAddress(customerId, addressId);
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
            long? customerId = Model !=null && Model.SelectedCustomer != null ? Model.SelectedCustomer.Id : (long?) null;
            long? addressId = Model != null && Model.SelectedAddress != null ? Model.SelectedAddress.Id : (long?)null;
            var result = CustomerServiceInstance.FindAll();
            Model = new AgendaModel { CustomerList = result.Customers };
            if(customerId.HasValue)
            {
                Model.SelectedCustomer = Model.CustomerList.FirstOrDefault(c => c.Id.Equals(customerId));
                if (Model.SelectedCustomer != null && addressId.HasValue)
                {
                    Model.SelectedAddress = Model.SelectedCustomer.Addresses.FirstOrDefault(a => a.Id.Equals(addressId));
                }
            }

            RaisePropertyChanged(() => Model);
        }

        private RelayCommand CreateCustomerCmdInstance;
        private RelayCommand UpdateCustomerCmdInstance;
        private RelayCommand CreateAddressCmdInstance;
        private RelayCommand UpdateAddressCmdInstance;

        public RelayCommand CreateCustomerCommand
        {
            get
            {
                if (CreateCustomerCmdInstance != null) return CreateCustomerCmdInstance;
                CreateCustomerCmdInstance = new RelayCommand(a => OpenCustomerDetail(null));
                return CreateCustomerCmdInstance;
            }
        }

        public RelayCommand UpdateCustomerCommand
        {
            get
            {
                if (UpdateCustomerCmdInstance != null) return UpdateCustomerCmdInstance;
                UpdateCustomerCmdInstance = new RelayCommand(a => OpenCustomerDetail(Model.SelectedCustomer), p => Model.SelectedCustomer != null);
                return UpdateCustomerCmdInstance;
            }
        }

        private void OpenCustomerDetail(CustomerDto customerDto)
        {
            var customerDetailViewModel = new CustomerDetailViewModel(customerDto);
            var result = customerDetailViewModel.ShowDialog();
            if (result.HasValue && result.Value) Model.SelectedCustomer = customerDetailViewModel.Model.Customer;
            Refresh();
        }

        public RelayCommand CreateAddressCommand
        {
            get
            {
                if (CreateAddressCmdInstance != null) return CreateAddressCmdInstance;
                CreateAddressCmdInstance = new RelayCommand(a => OpenAddressDetail(null), p => Model.SelectedCustomer != null);
                return CreateAddressCmdInstance;
            }
        }

        public RelayCommand UpdateAddressCommand
        {
            get
            {
                if (UpdateAddressCmdInstance != null) return UpdateAddressCmdInstance;
                UpdateAddressCmdInstance = new RelayCommand(a => OpenAddressDetail(Model.SelectedAddress), p => Model.SelectedAddress != null);
                return UpdateAddressCmdInstance;
            }
        }

        private void OpenAddressDetail(AddressDto addressDto)
        {
            if (addressDto == null) addressDto = new AddressDto{CustomerId = Model.SelectedCustomer.Id};
            var addressDetailViewModel = new AddressDetailViewModel((addressDto));
            var result = addressDetailViewModel.ShowDialog();
            if (result.HasValue && result.Value) Model.SelectedAddress = addressDetailViewModel.Model.Address;
            Refresh();
        }

        #endregion Commands

    }
}
