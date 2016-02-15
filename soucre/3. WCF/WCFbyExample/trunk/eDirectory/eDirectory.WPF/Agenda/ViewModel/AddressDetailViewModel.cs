using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.ServiceContract;
using eDirectory.WPF.Agenda.Model;
using eDirectory.WPF.Agenda.View;
using eDirectory.WPF.Core;
using eDirectory.WPF.Services;
using eDirectory.WPF.Util;

namespace eDirectory.WPF.Agenda.ViewModel
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    class AddressDetailViewModel
        :ViewModelBase
    {
        #region Private References

        private readonly AddressDetailView View;
        private readonly ServiceAdapter<IAddressService> AddressServiceAdapter;

        #endregion Private References
        #region Constructor

        public AddressDetailViewModel(AddressDto operation)
        {            
            AddressServiceAdapter = new ServiceAdapter<IAddressService>();
            Model = new AddressDetailModel {Address = new AddressDto()};
            Model.Address = operation;
            View = new AddressDetailView {DataContext = this};
        }

        #endregion Constructor
        #region Public Properties and Methods

        private bool IsInDialogMode;
        public bool? ShowDialog()
        {
            if (IsInDialogMode) return null;
            IsInDialogMode = true;
            return View.ShowDialog();
        }

        public AddressDetailModel Model { get; set; }

        #endregion Public Properties and Methods
        #region Commands

        private RelayCommand SaveCommandInstance;
        public RelayCommand SaveCommand
        {
            get
            {
                if (SaveCommandInstance != null) return SaveCommandInstance;
                SaveCommandInstance = new RelayCommand(a => Save(), p => Model.Address.IsValid());
                return SaveCommandInstance;
            }
        }

        private void Save()
        {
            if (Model.Address.Id == 0)
            {
                Model.Address = AddressServiceAdapter.Execute(s => s.CreateNewAddress(Model.Address));
            }
            else
            {
                Model.Address = AddressServiceAdapter.Execute(s => s.UpdateAddress(Model.Address));
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
