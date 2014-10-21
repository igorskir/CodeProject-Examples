using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;
using System.ServiceModel;

namespace eDirectory.UnitTests.WCF
{
    [ServiceContract(Namespace = "http://eDirectory/testservices/")]
    public interface ITestService
        : IContract
    {
        [OperationContract]
        DtoResponse MethodThrowsBusinessException();

        [OperationContract]
        DtoResponse MethodReturnsBusinessWarning();

        [OperationContract]
        DtoResponse MethodReturnsApplicationException();
    }
}
