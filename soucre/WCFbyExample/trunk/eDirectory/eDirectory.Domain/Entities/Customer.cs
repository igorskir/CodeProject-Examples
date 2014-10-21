using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Message;
using eDirectory.Domain.Repository;
using eDirectory.Common.Dto.Customer;
using System.Collections.ObjectModel;
//using Iesi.Collections.Generic;


namespace eDirectory.Domain.Entities
{
    /// <remarks>
    /// version 0.03 Chapter III:  The Response
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </remarks>
    [JsonObject(IsReference = true)]
    public class Customer
        :EntityBase
    {
        protected Customer()
        {
            AddressSet = new HashSet<Address>();
        }

        public virtual string FirstName { get; private set; }
        public virtual string LastName { get; private set; }
        public virtual string Telephone { get; private set; }

        protected virtual ICollection<Address> AddressSet { get; set; }

        public static Customer Create(IRepositoryLocator locator, CustomerDto operation)
        {
            ValidateOperation(locator, operation);
            CheckForDuplicates(locator, operation);
            var instance = new Customer
                            {
                                FirstName = operation.FirstName,
                                LastName = operation.LastName,
                                Telephone = operation.Telephone
                            };

            locator.Save(instance);
            return instance;
        }

        private static void CheckForDuplicates(IRepositoryLocator locator, CustomerDto op)
        {
            var result = locator.FindAll<Customer>().Where(c => c.FirstName == op.FirstName &&
                                                                c.LastName == op.LastName &&
                                                                c.Id != op.Id).ToArray();

            if (!result.Any()) return;
            throw new BusinessException(BusinessExceptionEnum.Validation, 
                                        "A customer instance already exist with these first and last names.");
        }

        public virtual void Update(IRepositoryLocator locator, CustomerDto operation)
        {
            ValidateOperation(locator, operation);
            CheckForDuplicates(locator, operation);
            FirstName = operation.FirstName;
            LastName = operation.LastName;
            Telephone = operation.Telephone;
            locator.Update(this);
        }
        
        public virtual ReadOnlyCollection<Address> Addresses()
        {
            if (AddressSet == null) return null;
            return new ReadOnlyCollection<Address>(AddressSet.ToArray());
        }

        public virtual Address AddAddress(IRepositoryLocator locator, AddressDto operation)
        {
            AddAddressValidate(locator, operation);
            var address = Address.Create(locator, operation);
            AddressSet.Add(address);
            return address;
        }

        private void AddAddressValidate(IRepositoryLocator locator, AddressDto operation)
        {            
            if(operation.CustomerId.Equals(Id)) return;
            throw new BusinessException(BusinessExceptionEnum.Validation, "Incorrect Customer Id was passed with the address details");
        }

        public virtual void DeleteAddress(IRepositoryLocator locator, long addressId)
        {
            DeleteAddressValidate(locator, addressId);
            var address = AddressSet.Single(a => a.Id.Equals(addressId));
            AddressSet.Remove(address);
            locator.Remove(address);
        }

        private void DeleteAddressValidate(IRepositoryLocator locator, long addressId)
        {
            return;
        }

        public class Mapping : EntityTypeConfiguration<Customer>
        {
            public Mapping()
            {
                HasMany(c => c.AddressSet).WithRequired(a => a.Customer);
            }
        }
    }
}
