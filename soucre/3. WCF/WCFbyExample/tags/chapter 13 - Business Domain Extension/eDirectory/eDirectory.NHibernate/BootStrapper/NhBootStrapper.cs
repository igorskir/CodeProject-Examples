using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.Domain.Entities;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace eDirectory.NHibernate.BootStrapper
{
    public class NhBootStrapper
    {
        private Configuration NhConfigurationInstance;

        public Configuration NhConfiguration
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationFileName)) throw new ArgumentException("ConfigurationFileName property was blank");
                NhConfigurationInstance = new Configuration();
                NhConfigurationInstance.Configure(ConfigurationFileName);
                NhConfigurationInstance.AddAssembly(typeof(Customer).Assembly);
                return NhConfigurationInstance;
            }
        }


        private SchemaExport eDirectorySchemaExportInstance;

        public SchemaExport eDirectorySchemaExport
        {
            get
            {
                if (eDirectorySchemaExportInstance != null) return eDirectorySchemaExportInstance;
                eDirectorySchemaExportInstance = new SchemaExport(NhConfiguration);
                return eDirectorySchemaExportInstance;
            }
        }


        public string ConfigurationFileName { get; set; }
    }
}
