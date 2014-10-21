using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.UnitTests.Core
{
    class TestBusinessExceptionManager
        :IBusinessExceptionManager
    {
        public void HandleBusinessException(BusinessExceptionDto exceptionDto)
        {
            throw new BusinessException(exceptionDto.ExceptionType, exceptionDto.Message);
        }
    }
}
