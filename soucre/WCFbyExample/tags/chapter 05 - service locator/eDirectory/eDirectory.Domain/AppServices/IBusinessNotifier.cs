using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.Domain.AppServices
{
    /// <remarks>
    /// version 0.5 Chapter V: Service Locator
    /// </remarks>
    public interface IBusinessNotifier
    {
        void AddWarning(BusinessWarningEnum warningType, string message);
        bool HasWarnings { get; }
        IEnumerable<BusinessWarning> RetrieveWarnings();
    }
}
