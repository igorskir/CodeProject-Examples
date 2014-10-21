using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using eDirectory.Common.Message;
using eDirectory.Common.ServiceContract;

namespace eDirectory.UnitTests.WCF
{
    public class WcfServiceHost
        :ICommandDispatcher

    {
        private static readonly IDictionary<Type, ServiceHost> ServiceHosts = new Dictionary<Type, ServiceHost>();
        private static readonly IDictionary<Type, ChannelFactory> ChannelFactories = new Dictionary<Type, ChannelFactory>();

        public static void StartService<TService, TInterface>() where TInterface : IContract
        {
            var serviceType = typeof(TService);
            var interfaceType = typeof(TInterface);
            if (ServiceHosts.ContainsKey(serviceType)) return;
            var strUri = @"net.pipe://localhost/" + interfaceType.Name;
            var instance = new ServiceHost(serviceType, new Uri(strUri));
            instance.AddServiceEndpoint(interfaceType, new NetNamedPipeBinding(), strUri);
            instance.Open();
            ServiceHosts.Add(interfaceType, instance);
        }

        public static void StopService<TInterface>()
        {
            var type = typeof(TInterface);
            if (!ServiceHosts.ContainsKey(type)) return;            
            var instance = ServiceHosts[type];
            StopChannel<TInterface>();
            if (instance.State != CommunicationState.Closed) instance.Close();
            ServiceHosts.Remove(type);
        }

        public static void StopChannel<TService>()
        {
            var type = typeof(TService);
            if (!ChannelFactories.ContainsKey(type)) return;
            var instance = ChannelFactories[type];
            try
            {
                // the state is not faulted if an exception takes place
                if (instance.State != CommunicationState.Closed && instance.State != CommunicationState.Faulted) instance.Close();
            }
            catch { }                        
            ChannelFactories.Remove(type);
        }

        public static ChannelFactory<TService> GetFactory<TService, TResult>(Func<TService, TResult> command)
            where TService : class, IContract
            where TResult : IDtoResponseEnvelop
        {
            var type = typeof(TService);
            if (ChannelFactories.ContainsKey(type)) return ChannelFactories[type] as ChannelFactory<TService>;
            var netPipeService = new ServiceEndpoint(
                ContractDescription.GetContract(type),
                new NetNamedPipeBinding(),
                new EndpointAddress("net.pipe://localhost/" + type.Name));

            var factory = new ChannelFactory<TService>(netPipeService);
            ChannelFactories.Add(type, factory);
            return factory;
        }

        #region Implementation of ICommandDispatcher

        public TResult ExecuteCommand<TService, TResult>(Func<TService, TResult> command) where TService : class, IContract where TResult : IDtoResponseEnvelop
        {
            var factory = GetFactory(command);
            var client = factory.CreateChannel();
            return command.Invoke(client);                          
        }

        #endregion
    }
}
