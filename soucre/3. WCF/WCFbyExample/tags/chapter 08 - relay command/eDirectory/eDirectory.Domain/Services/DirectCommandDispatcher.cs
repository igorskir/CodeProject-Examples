using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.Domain.Services
{
    public class DirectCommandDispatcher
        : ICommandDispatcher
    {
        #region Implementation of ICommandDispatcher<TService>

        public TResult ExecuteCommand<TService, TResult>(Func<TService, TResult> command)
            where TResult : IDtoResponseEnvelop
            where TService : class, IContract
        {
            var service = GetService<TService>();
            return command.Invoke(service);
        }

        #endregion

        private readonly IDictionary<Type, Type> _serviceMap = new Dictionary<Type, Type>
                                                                   {
                                                                       {typeof(ICustomerService), typeof(CustomerService)}
                                                                   };

        private TService GetService<TService>() where TService : class, IContract
        {
            var type = typeof(TService);
            if (!_serviceMap.ContainsKey(type))
            {
                var msg = "Implementation for contract: {0} was not defined in the dispatcher service map";
                msg = string.Format(msg, type.Name);
                throw new NotImplementedException(msg);
            }
            return (TService)Activator.CreateInstance(_serviceMap[type]);
        }
    }
}
