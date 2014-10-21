using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using eDirectory.WPF.Agenda.ViewModel;
using eDirectory.WPF.BootStrapper;
using eDirectory.WPF.Core;
using eDirectory.WPF.Customer.ViewModel;
using eDirectory.WPF.ExceptionNotifier.ViewModel;

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

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += AppDispatcherUnhandledException;
            base.OnStartup(e);            
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Current.DispatcherUnhandledException -= AppDispatcherUnhandledException;
            base.OnExit(e);
        }

        private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            if (e.Exception is SuspendProcessException) { return; }
            new ExceptionNotifierViewModel(e.Exception);
        }
    }
}
