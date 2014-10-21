using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using eDirectory.Domain.Entities;
using eDirectory.Domain.TransManager;

namespace eDirectory.Domain.Mappings
{
    public class ModelCreator:IModelCreator
    {
        #region Implementation of IModelCreator

        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new Customer.Mapping());                        
            modelBuilder.Entity<Address>();
        }

        #endregion
    }
}