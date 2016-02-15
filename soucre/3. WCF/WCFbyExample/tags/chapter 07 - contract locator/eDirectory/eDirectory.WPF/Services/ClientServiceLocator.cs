using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.WPF.Services
{
    /// <remarks>
    /// version 0.7 Chapter VII: Contract Locator
    /// </remarks>
    public class ClientServiceLocator
    {
        static readonly Object LocatorLock = new object();
        private static ClientServiceLocator InternalInstance;

        private ClientServiceLocator() { }

        public static ClientServiceLocator Instance()
        {
            if (InternalInstance == null)
            {
                lock (LocatorLock)
                {
                    // in case of a race scenario ... check again
                    if (InternalInstance == null)
                    {
                        InternalInstance = new ClientServiceLocator();
                    }
                }
            }
            return InternalInstance;
        }

        public ICommandDispatcher CommandDispatcher { get; set; }
    }
}
