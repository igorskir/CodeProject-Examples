using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.AppServices;

namespace eDirectory.Naive.AppServices
{
    /// <remarks>
    /// version 0.71 Context Re-Factor
    /// </remarks>
    public class RequestContextNaive
        :IRequestContext
    {
        private BusinessNotifier BusinessNotifierInstance;

        public IBusinessNotifier Notifier
        {
            get 
            {
                if (BusinessNotifierInstance != null) return BusinessNotifierInstance;
                BusinessNotifierInstance = new BusinessNotifier();
                return BusinessNotifierInstance;
            }
        }
    }
}
