using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using eDirectory.Common.DependencyInjection;
using eDirectory.Domain.Entities;
using Spring.Context.Support;
using System.ServiceModel;
using System.Web;
using Spring.Context.Support;
using System.ServiceModel.Activation;

namespace eDirectory.WcfService
{
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    public class ServiceFactory 
        : ServiceHostFactory
    {
        
        static readonly Object ServiceLock = new object();
        static bool IsInitialised;

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            if (!IsInitialised) InitialiseService();
            return base.CreateServiceHost(serviceType, baseAddresses);
        }

        private void InitialiseService()
        {
            lock (ServiceLock)
            {
                // check again ... cover for a race scenario
                if (IsInitialised) return;
                InitialiseDependecy();
                AutoMapperConfigurator.Install();
                IsInitialised = true;
            }
        }

        private void InitialiseDependecy()
        {
            string spring = @"file://" + HttpRuntime.AppDomainAppPath + ConfigurationManager.AppSettings.Get("SpringConfigFile");
            DiContext.AppContext = new XmlApplicationContext(spring);
        }

    }

}