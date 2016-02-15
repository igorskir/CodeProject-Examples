using System.Data.Entity;
using eDirectory.Domain.TransManager;

namespace eDirectory.EF.TransManager
{
    public class eDirectoryDbContext : DbContext
    {
        public IModelCreator ModelCreator { get; private set; }

        public eDirectoryDbContext(IModelCreator modelCreator): base("eDirectory")
        {
            ModelCreator = modelCreator;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ModelCreator.OnModelCreating(modelBuilder);            
        }
    }
}