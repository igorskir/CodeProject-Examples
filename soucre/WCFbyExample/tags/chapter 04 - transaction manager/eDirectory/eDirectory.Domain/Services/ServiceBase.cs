using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;
using eDirectory.Domain.Repository;
using eDirectory.Common.Message;

namespace eDirectory.Domain.Services
{
    /// <remarks>
    /// version 0.4 Chapter IV: Transaction Manager
    /// </remarks>
    public class ServiceBase
    {
        public ITransFactory Factory { get; set; }

        protected TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) 
            where TResult : class, IDtoResponseEnvelop
        {
            using (ITransManager manager = Factory.CreateManager())
            {
                return manager.ExecuteCommand(command);
            }
        }
    }
}
