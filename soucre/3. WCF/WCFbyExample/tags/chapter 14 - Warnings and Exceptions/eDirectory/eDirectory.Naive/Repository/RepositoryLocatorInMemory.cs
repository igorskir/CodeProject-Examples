using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;

namespace eDirectory.Naive.Repository
{
    /// <remarks>
    /// version 0.02 Chapter II: Repository
    /// version 0.10 Chapter X: Renamed from RepositoryLocatorEntityStore to RepositoryLocatorInMemory
    /// version 0.10 Chapter X: IResetable implementation
    /// </remarks>
    public class RepositoryLocatorInMemory
        : RepositoryLocatorBase, IResetable
    {
        protected override IRepository<T> CreateRepository<T>()
        {
            return new RepositoryInMemory<T>();
        }

        public void Reset()
        {
            RepositoryMap = new Dictionary<Type, object>();
        }
    }
}
