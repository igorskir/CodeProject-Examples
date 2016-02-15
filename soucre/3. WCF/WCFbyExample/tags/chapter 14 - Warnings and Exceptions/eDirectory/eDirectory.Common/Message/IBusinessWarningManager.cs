using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Common.Message
{
    public interface IBusinessWarningManager
    {
        void HandleBusinessWarning(IEnumerable<BusinessWarning> warnings);
    }
}
