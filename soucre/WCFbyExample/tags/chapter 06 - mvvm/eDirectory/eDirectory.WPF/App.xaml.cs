using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using eDirectory.WPF.BootStrapper;
using eDirectory.WPF.Customer.ViewModel;

namespace eDirectory.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void BootStrapper(object sender, StartupEventArgs e)
        {
            var boot = new eDirectoryBootStrapper();
            boot.Run();
            new CustomerViewModel();
        }
    }
}
