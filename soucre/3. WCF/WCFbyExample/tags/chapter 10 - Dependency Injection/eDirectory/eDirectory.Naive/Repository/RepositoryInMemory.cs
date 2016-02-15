using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using eDirectory.Domain.Entities;
using System.Reflection;

namespace eDirectory.Naive.Repository
{
    /// <remarks>
    /// version 0.02 Chapter II: Repository
    /// version 0.10 Chapter X: Renamed from RepositoryEntityStore to RepositoryInMemory
    /// </remarks>
    public class RepositoryInMemory<TEntity>
        :IRepository<TEntity>
    {
        protected readonly IDictionary<long, TEntity> RepositoryMap = new Dictionary<long, TEntity>();

        #region IRepository<TEntity> Members

        public TEntity Save(TEntity instance)
        {
            IEntity entityInstance = GetEntityInstance(instance);
            if (entityInstance.Id != 0)
            {
                throw new ApplicationException("Entity instance cannot be saved, the Id field was not cero");
            }
            GetNewId(instance);
            RepositoryMap.Add(entityInstance.Id, instance);
            return instance;
        }

        public void Update(TEntity instance)
        {
            IEntity entityInstance = GetEntityInstance(instance);
            if (entityInstance.Id == 0)
            {
                throw new ApplicationException("Entity instance cannot be updated, the Id field was cero");
            }
            RepositoryMap[entityInstance.Id] = instance;
        }

        public void Remove(TEntity instance)
        {
            IEntity entityInstance = GetEntityInstance(instance);
            RepositoryMap.Remove(entityInstance.Id);
        }

        public TEntity GetById(long id)
        {
            return RepositoryMap[id];
        }

        public IQueryable<TEntity> FindAll()
        {
            return RepositoryMap.Values.AsQueryable();
        }

        #endregion

        #region Helper Methods

        private void GetNewId(TEntity instance)
        {
            GetEntityInstance(instance);
            long max = 0;
            if (RepositoryMap.Count > 0) max = RepositoryMap.Keys.Max();
            max = ++max;
            var setter = GetSetter(instance.GetType());
            setter.Invoke(instance, new object[] { max });
        }

        private readonly IDictionary<Type, MethodInfo> Setters = new Dictionary<Type, MethodInfo>();

        private MethodInfo GetSetter(Type type)
        {
            if (Setters.ContainsKey(type)) return Setters[type];
            var setter = type.GetProperty("Id").GetSetMethod(true);
            Setters.Add(type, setter);
            return setter;
        }

        private IEntity GetEntityInstance(TEntity instance)
        {
            var entityInstance = instance as IEntity;
            if (entityInstance == null) throw new ArgumentException("Passed instance is not an IEntity");
            return entityInstance;
        }

        #endregion
    }
}
