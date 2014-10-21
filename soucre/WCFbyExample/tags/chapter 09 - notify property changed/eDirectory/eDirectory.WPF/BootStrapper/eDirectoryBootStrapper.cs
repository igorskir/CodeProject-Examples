using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.AppServices;
using eDirectory.Domain.Services;
using eDirectory.Naive.TransManager;
using eDirectory.WPF.Services;
using eDirectory.Naive.AppServices;

namespace eDirectory.WPF.BootStrapper
{
    class eDirectoryBootStrapper
    {
        public void Run()
        {
            InitialiseDependencies();
        }

        private void InitialiseDependencies()
        {
            GlobalContext.Instance().TransFactory = new TransManagerEntityStoreFactory();
            Container.RequestContext = new RequestContextNaive();      
            ClientServiceLocator.Instance().CommandDispatcher = new DirectCommandDispatcher();
        }
    }
}
