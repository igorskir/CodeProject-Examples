using System.Data.Entity;

namespace eDirectory.Domain.TransManager
{
    public interface IModelCreator
    {
        void OnModelCreating(DbModelBuilder modelBuilder);
    }
}