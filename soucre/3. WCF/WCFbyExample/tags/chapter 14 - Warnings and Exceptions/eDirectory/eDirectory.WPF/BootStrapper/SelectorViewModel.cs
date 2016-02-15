using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.WPF.Agenda.ViewModel;
using eDirectory.WPF.Core;
using eDirectory.WPF.Customer.ViewModel;
using eDirectory.WPF.Util;

namespace eDirectory.WPF.BootStrapper
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    class SelectorViewModel
        :ViewModelBase
    {
        private SelectorView View;

        public SelectorViewModel()
        {
            View = new SelectorView{DataContext = this};
            CurrentOption = ViewTypeEnum.CustomerAddressView;
        }

        public ViewTypeEnum CurrentOption { get; set; }

        private RelayCommand OkCommandInstance;
        public RelayCommand OkCommand
        {
            get
            {
                if (OkCommandInstance != null) return OkCommandInstance;
                OkCommandInstance = new RelayCommand(a => View.Close());
                return OkCommandInstance;
            }
        }

        public ViewTypeEnum GetViewType()
        {
            View.ShowDialog();
            return CurrentOption;
        }
    }

    public enum ViewTypeEnum
    {
        CustomerView,
        CustomerAddressView
    }
}
