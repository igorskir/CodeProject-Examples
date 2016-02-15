using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eDirectory.Domain.TransManager
{
    /// <remarks>
    /// version 0.4 Chapter IV: Transaction Manager
    /// </remarks>
    public interface ITransFactory
    {
        ITransManager CreateManager();
    }
}
