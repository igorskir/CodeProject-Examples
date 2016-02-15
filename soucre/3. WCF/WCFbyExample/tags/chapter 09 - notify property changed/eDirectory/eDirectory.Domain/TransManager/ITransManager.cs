using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using eDirectory.Common.Message;

namespace eDirectory.Domain.TransManager
{
    /// <remarks>
    /// version 0.4 Chapter IV: Transaction Manager
    /// </remarks>
    public interface ITransManager
        : IDisposable
    {
        TResult ExecuteCommand<TResult>(Func<IRepositoryLocator, TResult> command) 
            where TResult : class, IDtoResponseEnvelop;

        void BeginTransaction();
        void CommitTransaction();
        void Rollback();
    }
}
