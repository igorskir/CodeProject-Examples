using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using eDirectory.Common.Dto.Customer;
using eDirectory.Common.Message;

namespace eDirectory.Common.ServiceContract
{
    /// <summary>
    /// Exposes the customer services
    /// </summary>
    /// <remarks>
    /// version 0.03 Chapter III:  The Response
	/// version 0.07 Chapter VII:  Contract Locator
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </remarks>
    [ServiceContract(Namespace = "http://wcfbyexample/customerservices/")]
    public interface ICustomerService
        :IContract
    {

        [OperationContract]
        CustomerDto CreateNewCustomer(CustomerDto customer);

        [OperationContract]
        CustomerDto GetById(long id);

        [OperationContract]
        CustomerDto UpdateCustomer(CustomerDto customer);

        [OperationContract]
        CustomerDtos FindAll();

        [OperationContract]
        DtoResponse DeleteCustomer(long id);

        [OperationContract]
        DtoResponse DeleteAddress(long customerId, long addressId);
    }
}
