﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using eDirectory.Common.ServiceContract;
using eDirectory.Common.Message;
using System.Windows.Forms;
using eDirectory.WPF.Core;
using eDirectory.WPF.ExceptionNotifier.ViewModel;
using Hardcodet.Wpf.TaskbarNotification;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.07 Chapter VII:  Contract Locator
    /// version 0.13 Chapter XIII: Async Service Commands
    /// </remarks>
    public class ServiceAdapterBase<TService> where TService: IContract
    {
        protected TService Service;

        protected ServiceAdapterBase(TService service)
        {
            Service = service;
        }              
        
        public TResult ExecuteCommand<TResult>(Func<TResult> command) where TResult : IDtoResponseEnvelop
        {
            try
            {
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                TResult result = DispatchCommand(command);
                if (result.Response.HasWarning)
                {
                    ClientServiceLocator.Instance()
                                        .WarningManager
                                        .HandleBusinessWarning(result.Response.BusinessWarnings);

                }
                if (result.Response.HasException)
                {
                    Mouse.OverrideCursor = null;                    
                    ClientServiceLocator.Instance()
                                        .ExceptionManager
                                        .HandleBusinessException(result.Response.BusinessException);
                }
                return result;
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private static TResult DispatchCommand<TResult>(Func<TResult> command)
        {
            var asynchResult = command.BeginInvoke(null, null);
            while (!asynchResult.IsCompleted)
            {
                Application.DoEvents();
                Thread.CurrentThread.Join(50);
            }
            return command.EndInvoke(asynchResult);
        }
    }
}
