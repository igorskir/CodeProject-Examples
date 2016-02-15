using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.TransManager;

namespace eDirectory.Domain.AppServices
{
    /// <remarks>
    /// version 0.50 Chapter V: Service Locator
    /// version 0.71 Context Re-Factor 
    /// </remarks>
    public interface IGlobalContext
    {
        ITransFactory TransFactory { get; }        
    }
}
