using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using eDirectory.Common.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace eDirectory.UnitTests.BootStrapper
{
    
    [TestClass]
    public class TestsBootStrapper
    {
        [AssemblyInitialize]

        public static void Run(TestContext context)
        {
            InitialiseDependencies(context);
        }

        private static void InitialiseDependencies(TestContext context)
        {
            var spring = ConfigurationManager.AppSettings.Get("SpringConfigFile");
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = @"file://" + Path.Combine(basePath, spring);
            DiContext.AppContext = new XmlApplicationContext(filePath);
        }
    }
}
