using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Repository;
using eDirectory.Common.Dto.Customer;


namespace eDirectory.Domain.Entities
{
    /// <remarks>
    /// version 0.1 Chapter: Introduction
    /// </remarks>
    public class Customer
    {
        protected Customer() { }

        public long Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Telephone { get; protected set; }

        public static Customer Create(IRepository<Customer> repository, CustomerDto operation)
        {
            var instance = new Customer
                            {
                                FirstName = operation.FirstName,
                                LastName = operation.LastName,
                                Telephone = operation.Telephone
                            };

            repository.Save(instance);
            return instance;
        }

    }
}
