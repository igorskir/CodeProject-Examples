using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;

namespace eDirectory.UnitTests.Core
{
    class TestBusinessWarningManager
        :IBusinessWarningManager
    {
        public void HandleBusinessWarning(IEnumerable<BusinessWarning> warnings)
        {
            return;
        }
    }
}
