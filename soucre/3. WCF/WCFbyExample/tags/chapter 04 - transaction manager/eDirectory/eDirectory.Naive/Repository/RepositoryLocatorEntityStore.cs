using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;

namespace eDirectory.Naive.Repository
{
    /// <remarks>
    /// version 0.2 Chapter II: Repository
    /// </remarks>
    public class RepositoryLocatorEntityStore
        : RepositoryLocatorBase
    {
        protected Dictionary<Type, object> RepositoryMap = new Dictionary<Type, object>();

        public override IRepository<T> GetRepository<T>()
        {
            var type = typeof(T);
            if (RepositoryMap.Keys.Contains(type)) return RepositoryMap[type] as IRepository<T>;
            var repository = new RepositoryEntityStore<T>();
            RepositoryMap.Add(type, repository);
            return repository;
        }
    }
}
