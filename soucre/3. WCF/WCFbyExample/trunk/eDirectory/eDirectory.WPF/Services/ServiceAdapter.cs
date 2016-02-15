using System;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    public class ServiceAdapter<TService>
        where TService: class, IContract
    {
        public TResult Execute<TResult>(Func<TService, TResult> command) 
            where TResult : IDtoResponseEnvelop
        {
            try
            {                                
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                var dispatcher = ClientServiceLocator.Instance().CommandDispatcher;
                var result = DispatchCommand(() => dispatcher.ExecuteCommand(command));
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

        private static TResult DispatchCommand<TResult>(Func<TResult> dispatcherCommand) 
            where TResult : IDtoResponseEnvelop
        {            
            var asynchResult = dispatcherCommand.BeginInvoke(null, null);
            while (!asynchResult.IsCompleted)
            {
                Application.DoEvents();
                Thread.CurrentThread.Join(50);
            }
            return dispatcherCommand.EndInvoke(asynchResult);
        }
    }
}