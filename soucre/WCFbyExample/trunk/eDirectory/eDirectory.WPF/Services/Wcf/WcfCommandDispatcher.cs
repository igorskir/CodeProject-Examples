using System;
using System.ServiceModel;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services.Wcf
{
    public class WcfCommandDispatcher        
        : ICommandDispatcher

    {
        public TResult ExecuteCommand<TService, TResult>(Func<TService, TResult> command)
            where TResult : IDtoResponseEnvelop 
            where TService : class, IContract
        {
            var proxy = new WcfProxy<TService>();
            try
            {
                var result = command.Invoke(proxy.WcfChannel);
                proxy.Close();
                return result;
            }
            catch (Exception)
            {
                proxy.Abort();
                throw;
            }
        }

        private class WcfProxy<TService> :
            ClientBase<TService> where TService : class, IContract
        {
            public TService WcfChannel
            {
                get
                {
                    return Channel;
                }
            }
        }
    }
}