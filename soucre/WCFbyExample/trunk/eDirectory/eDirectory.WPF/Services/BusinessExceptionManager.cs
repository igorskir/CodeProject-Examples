using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.WPF.Core;
using eDirectory.WPF.ExceptionNotifier.ViewModel;

namespace eDirectory.WPF.Services
{
    class BusinessExceptionManager
        :IBusinessExceptionManager
    {
        public void HandleBusinessException(BusinessExceptionDto exceptionDto)
        {
            new ExceptionNotifierViewModel(exceptionDto);
            throw new SuspendProcessException();
        }
    }
}
