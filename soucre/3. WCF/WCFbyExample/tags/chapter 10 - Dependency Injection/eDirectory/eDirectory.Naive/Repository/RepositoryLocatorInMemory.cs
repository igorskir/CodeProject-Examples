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
        protected Dictionary<Type, object> RepositoryMap = new Dictionary<Type, object>();

        public override IRepository<T> GetRepository<T>()
        {
            var type = typeof(T);
            if (RepositoryMap.Keys.Contains(type)) return RepositoryMap[type] as IRepository<T>;
            var repository = new RepositoryInMemory<T>();
            RepositoryMap.Add(type, repository);
            return repository;
        }

        public void Reset()
        {
            RepositoryMap = new Dictionary<Type, object>();
        }
    }
}
