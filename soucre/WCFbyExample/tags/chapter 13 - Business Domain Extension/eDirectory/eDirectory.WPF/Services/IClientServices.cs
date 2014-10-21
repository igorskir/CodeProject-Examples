using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.7 Chapter VII: Contract Locator
    /// </remarks>
    interface IClientServices
    {
        IContractLocator ContractLocator { get; }
    }
}
