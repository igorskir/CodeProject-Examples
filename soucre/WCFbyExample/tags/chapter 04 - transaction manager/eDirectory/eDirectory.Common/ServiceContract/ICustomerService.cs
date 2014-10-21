﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using eDirectory.Common.Dto.Customer;

namespace eDirectory.Common.ServiceContract
{
    /// <summary>
    /// Exposes the customer services
    /// </summary>
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    [ServiceContract(Namespace = "http://wcfbyexample/customerservices/")]
    public interface ICustomerService
    {

        [OperationContract]
        CustomerDto CreateNewCustomer(CustomerDto customer);

        [OperationContract]
        CustomerDto GetById(long id);

        [OperationContract]
        CustomerDto UpdateCustomer(CustomerDto customer);

        [OperationContract]
        CustomerDtos FindAll();
    }
}
