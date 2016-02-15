using System.Configuration;
using eDirectory.Common.DependencyInjection;
using Spring.Context.Support;

namespace eDirectory.WPF.BootStrapper
{
    /// <remarks>
    /// version 0.08 Chapter VIII: Relay Command
    /// version 0.10 Chapter X: Dependency Injection
    /// </remarks>
    class eDirectoryBootStrapper
    {
        public void Run()
        {
            InitialiseDependencies();
        }

        private void InitialiseDependencies()
        {
            var spring = ConfigurationManager.AppSettings.Get("SpringConfigFile");
            DiContext.AppContext = new XmlApplicationContext(spring);
        }
    }
}
