using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;
using eDirectory.Domain.Repository;
using eDirectory.Common.Message;
using eDirectory.Domain.AppServices;

namespace eDirectory.Domain.Services
{
    /// <remarks>
    /// version 0.50 Chapter V: Service Locator
    /// version 0.71 Context Re-Factor
    /// </remarks>
    public class ServiceBase
    {

        protected TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) 
            where TResult : class, IDtoResponseEnvelop
        {
            using (ITransManager manager = Container.GlobalContext.TransFactory.CreateManager())
            {
                return manager.ExecuteCommand(command);
            }
        }
    }
}
