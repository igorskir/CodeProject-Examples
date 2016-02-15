using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using eDirectory.WPF.Agenda.ViewModel;
using eDirectory.WPF.BootStrapper;
using eDirectory.WPF.Customer.ViewModel;

namespace eDirectory.WPF
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    public partial class App : Application
    {
        
        public App()
        {
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        private void BootStrapper(object sender, StartupEventArgs e)
        {
            var boot = new eDirectoryBootStrapper();
            boot.Run();            
            Shutdown();
        }
    }
}
