using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.Message
{
    public interface IBusinessExceptionManager
    {
        void HandleBusinessException(BusinessExceptionDto exceptionDto);
    }
}
