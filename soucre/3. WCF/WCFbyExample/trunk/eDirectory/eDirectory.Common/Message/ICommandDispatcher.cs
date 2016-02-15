using System;
using eDirectory.Common.ServiceContract;

namespace eDirectory.Common.Message
{
    public interface ICommandDispatcher
    {
        TResult ExecuteCommand<TService, TResult>(Func<TService, TResult> command)
            where TResult : IDtoResponseEnvelop
            where TService : class, IContract;

    }
}