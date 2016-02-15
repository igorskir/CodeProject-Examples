using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace eDirectory.Common.Message
{
    /// <remarks>
    /// version 0.03 Chapter III: The Response
    /// version 0.11 Chapter XI: DataContract attributes were added
    /// </remarks>
    [DataContract]
    public class BusinessWarning
    {
        public BusinessWarning() { }
        public BusinessWarning(BusinessWarningEnum warningType, string message)
        {
            ExceptionType = warningType;
            Message = message;
        }

        [DataMember]
        public BusinessWarningEnum ExceptionType { get; private set; }
        [DataMember]
        public string Message { get; private set; }
    }
}
