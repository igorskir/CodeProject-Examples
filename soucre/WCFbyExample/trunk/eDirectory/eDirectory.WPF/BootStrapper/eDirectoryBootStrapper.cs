using System.Configuration;
using eDirectory.Common.DependencyInjection;
using eDirectory.WPF.Agenda.ViewModel;
using eDirectory.WPF.Customer.ViewModel;
using Spring.Context.Support;

namespace eDirectory.WPF.BootStrapper
{
    /// <remarks>
    /// version 0.08 Chapter VIII: Relay Command
    /// version 0.10 Chapter X:    Dependency Injection
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </remarks>
    class eDirectoryBootStrapper
    {
        public void Run()
        {
            InitialiseDependencies();
            var result = (new SelectorViewModel()).GetViewType();
            if (result.Equals(ViewTypeEnum.CustomerAddressView))
            {
                new AgendaViewModel();
            }
            else
            {
                new CustomerViewModel();
            }
        }

        private void InitialiseDependencies()
        {
            var spring = ConfigurationManager.AppSettings.Get("SpringConfigFile");
            DiContext.AppContext = new XmlApplicationContext(spring);
        }
    }
}
