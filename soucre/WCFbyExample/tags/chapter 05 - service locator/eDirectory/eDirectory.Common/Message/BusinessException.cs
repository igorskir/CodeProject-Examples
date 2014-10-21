using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.Message
{
    /// <remarks>
    /// version 0.4 Chapter IV: Transaction Manager
    /// </remarks>
    public class BusinessException
            : ApplicationException
    {
        public BusinessException(BusinessExceptionEnum businessExceptionType, string message)
            : base(message)
        {
            ExceptionType = businessExceptionType;
        }

        public BusinessExceptionEnum ExceptionType { get; set; }
    }
}
