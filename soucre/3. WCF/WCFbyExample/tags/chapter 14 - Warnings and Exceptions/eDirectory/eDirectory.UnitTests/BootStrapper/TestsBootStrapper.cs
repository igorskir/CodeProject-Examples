using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using eDirectory.Common.DependencyInjection;
using eDirectory.Domain.Entities;
using eDirectory.UnitTests.Core;
using eDirectory.WPF.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spring.Context.Support;

namespace eDirectory.UnitTests.BootStrapper
{
    
    /// <summary>
    /// version 0.13 Chapter XIII: Business Domain Extension
    /// </summary>
    [TestClass]
    public class TestsBootStrapper
    {
        [AssemblyInitialize]
        public static void Run(TestContext context)
        {
            InitialiseDependencies(context);
            InstallAutoMapperDefinitions();
        }

        private static void InstallAutoMapperDefinitions()
        {
            AutoMapperConfigurator.Install();
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
