using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.Message
{
    /// <remarks>
    /// version 0.3 Chapter III: The Response
    /// </remarks>
    public class BusinessWarning
    {
        public BusinessWarning() { }
        public BusinessWarning(BusinessWarningEnum warningType, string message)
        {
            ExceptionType = warningType;
            Message = message;
        }

        public BusinessWarningEnum ExceptionType { get; private set; }
        public string Message { get; private set; }
    }
}
