using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eDirectory.NHibernate.BootStrapper;

namespace eDirectory.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var nhBootStrapper = new NhBootStrapper
                                     {
                                         ConfigurationFileName = @"nhibernate.cfg.xml"
                                     };

            var connString = nhBootStrapper.NhConfiguration.GetProperty("connection.connection_string");
            System.Console.WriteLine("Updating database schema for: " + connString);
            nhBootStrapper.eDirectorySchemaExport.Create(true, true);
        }
    }
}
