using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using eDirectory.Common.Dto.Customer;


namespace eDirectory.Domain.Entities
{
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    public class Customer
        :EntityBase
    {
        protected Customer() { }

        public virtual string FirstName { get; protected set; }
        public virtual string LastName { get; protected set; }
        public virtual string Telephone { get; protected set; }

        public static Customer Create(IRepositoryLocator locator, CustomerDto operation)
        {
            var instance = new Customer
                            {
                                FirstName = operation.FirstName,
                                LastName = operation.LastName,
                                Telephone = operation.Telephone
                            };

            locator.Save(instance);
            return instance;
        }

        public virtual void Update(IRepositoryLocator locator, CustomerDto operation)
        {
            UpdateValidate(locator, operation);
            FirstName = operation.FirstName;
            LastName = operation.LastName;
            Telephone = operation.Telephone;
            locator.Update(this);
        }

        private void UpdateValidate(IRepositoryLocator locator, CustomerDto operation)
        {
            return;
        }

    }
}
