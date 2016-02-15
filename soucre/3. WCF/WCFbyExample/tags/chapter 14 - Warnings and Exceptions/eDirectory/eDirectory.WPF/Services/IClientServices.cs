using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.07 Chapter VII: Contract Locator
    /// version 0.14 Chapter XIV: Business Exception & Warnings
    /// </remarks>
    interface IClientServices
    {
        IContractLocator ContractLocator { get; }
        IBusinessExceptionManager ExceptionManager { get; }
    }
}
