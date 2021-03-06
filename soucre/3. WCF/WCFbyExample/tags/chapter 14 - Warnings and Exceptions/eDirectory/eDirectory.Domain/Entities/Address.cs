﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Dto.Address;
using eDirectory.Common.Message;
using eDirectory.Domain.Repository;

namespace eDirectory.Domain.Entities
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// version 0.14 Chapter XIV:  Validation
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
            ValidateOperation(locator,operation);
            CheckForDuplicates(locator, operation);
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
            ValidateOperation(locator, operation);
            CheckForDuplicates(locator, operation);
            Street = operation.Street;
            City = operation.City;
            PostCode = operation.PostCode;
            Country = operation.Country;
            locator.Update(this);
        }


        private static void CheckForDuplicates(IRepositoryLocator locator, AddressDto op)
        {
            var result = locator.FindAll<Address>().Where(a =>  a.Customer.Id.Equals(op.CustomerId) &&
                                                                a.Street.Equals(op.Street) &&
                                                                a.City.Equals(op.City) &&
                                                                a.PostCode.Equals(op.PostCode) &&
                                                                a.Country.Equals(op.Country) &&
                                                                a.Id != op.Id);

            if (result.Count() == 0) return;
            throw new BusinessException(BusinessExceptionEnum.Validation,
                                        "An address instance for this customer already exist with the same details.");
        }

    }
}
