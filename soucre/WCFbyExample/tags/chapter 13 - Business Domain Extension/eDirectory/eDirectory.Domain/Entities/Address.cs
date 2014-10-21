using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Domain.Repository;

namespace eDirectory.Domain.Entities
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    public class Address
        :EntityBase
    {
        protected Address(){}

        public virtual Customer Customer { get; private set; }
        public virtual string Street { get; private set; }
        public virtual string City { get; private set; }
        public virtual string PostCode { get; private set; }
        public virtual string Country { get; private set; }

        public static Address Create(IRepositoryLocator locator, AddressDto operation)
        {
            var customer = locator.GetById<Customer>(operation.CustomerId);
            var instance = new Address
            {
                Customer = customer,
                Street = operation.Street,
                City = operation.City,
                PostCode = operation.PostCode,
                Country = operation.Country
            };

            locator.Save(instance);
            return instance;
        }

        public virtual void Update(IRepositoryLocator locator, AddressDto operation)
        {
            UpdateValidate(locator, operation);
            Street = operation.Street;
            City = operation.City;
            PostCode = operation.PostCode;
            Country = operation.Country;
            locator.Update(this);
        }

        private void UpdateValidate(IRepositoryLocator locator, AddressDto operation)
        {
            return;
        }
    }
}
