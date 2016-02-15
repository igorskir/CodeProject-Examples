using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace eDirectory.Common.Message
{
    /// <remarks>
    /// version 0.03 Chapter III: The Response
    /// version 0.11 Chapter IX : Add DataContract attributes
    /// </remarks>
    [DataContract]
    public class BusinessExceptionDto
    {
        protected BusinessExceptionDto() { }

        public BusinessExceptionDto(BusinessExceptionEnum type, string message, string stackTrace)
        {
            ExceptionType = type;
            Message = message;
            StackTrace = stackTrace;
        }

        [DataMember]
        public BusinessExceptionEnum ExceptionType { get; private set; }
        [DataMember]
        public string Message { get; private set; }
        [DataMember]
        public string StackTrace { get; private set; }
    }
}
