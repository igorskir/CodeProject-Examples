using System;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    public class ServiceAdapter<TService>
        where TService:class, IContract
    {
        public TResponse Execute<TResponse>(Func<TService, TResponse> command)
            where TResponse : IDtoResponseEnvelop
        {
            var dispatcher = ClientServiceLocator.Instance().CommandDispatcher;
            var result = dispatcher.ExecuteCommand(command);
            if (result.Response.HasWarning)
            {
                // TODO: Implement Business Warning processing -- See Chapter XIV

            }
            if (result.Response.HasException)
            {
                // TODO: Implement Business Exception processing -- See Chapter XIV
            }
            return result;
        }        
    }
}