using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Domain.AppServices;
using eDirectory.Domain.Repository;
using eDirectory.Domain.Services;

namespace eDirectory.UnitTests.WCF
{
    public class TestService
            : ServiceBase, ITestService
    {
        #region Implementation of ITestService

        public DtoResponse MethodThrowsBusinessException()
        {
            return ExecuteCommand<DtoResponse>(arg => MethodThrowsBusinessExceptionCommand());
        }

        private DtoResponse MethodThrowsBusinessExceptionCommand()
        {
            throw new BusinessException(BusinessExceptionEnum.Operational, "Business Exception was thrown");
        }

        public DtoResponse MethodReturnsBusinessWarning()
        {
            return ExecuteCommand(arg => MethodReturnsBusinessWarningCommand());
        }

        private DtoResponse MethodReturnsBusinessWarningCommand()
        {
            Container.RequestContext.Notifier.AddWarning(BusinessWarningEnum.Operational, "Warning was added");
            return new DtoResponse();
        }

        public DtoResponse MethodReturnsApplicationException()
        {
            return ExecuteCommand<DtoResponse>(arg => MethodReturnsApplicationExceptionCommand());
        }

        private DtoResponse MethodReturnsApplicationExceptionCommand()
        {
            throw new ApplicationException("Application Exception was thrown");
        }

        #endregion
    }
}
