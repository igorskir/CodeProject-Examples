using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Entities;
using System.Reflection;

namespace eDirectory.Naive.Repository
{
    /// <remarks>
    /// version 0.1 Chapter: Introduction
    /// </remarks>
    public class RepositoryCustomer
        : RepositoryEntityStore<Customer>
    {
        public override Customer Save(Customer instance)
        {
            if (instance.Id != 0)
            {
                throw new ApplicationException("Entity instance cannot be saved, the Id field was not cero");
            }
            GetNewId(instance);
            RepositoryMap.Add(instance.Id, instance);
            return instance;
        }

        public override void Update(Customer instance)
        {
            if (instance.Id == 0)
            {
                throw new ApplicationException("Entity instance cannot be updated, the Id field was cero");
            }
            RepositoryMap[instance.Id] = instance;
        }

        public override void Remove(Customer instance)
        {
            RepositoryMap.Remove(instance.Id);
        }

        private void GetNewId(Customer instance)
        {
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
    }
}
