﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Resources;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;
using Hardcodet.Wpf.TaskbarNotification;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.07 Chapter VII: Contract Locator
    /// version 0.10 Chapter X:   Dependency Injection
    /// version 0.14 Chapter XIV: Business Exception & Warnings
    /// </remarks>
    public class ClientServiceLocator
        : IClientServices
    {
        static readonly Object LocatorLock = new object();
        private static ClientServiceLocator InternalInstance;

        private ClientServiceLocator() { }

        public static ClientServiceLocator Instance()
        {
            if (InternalInstance == null)
            {
                lock (LocatorLock)
                {
                    // in case of a race scenario ... check again
                    if (InternalInstance == null)
                    {
                        InternalInstance = new ClientServiceLocator();
                    }
                }
            }
            return InternalInstance;
        }

        #region IClientServices Members

        public IContractLocator ContractLocator { get; private set; }
        public IBusinessExceptionManager ExceptionManager { get; set; }
        public IBusinessWarningManager WarningManager { get; set; }

        #endregion

    }
}
