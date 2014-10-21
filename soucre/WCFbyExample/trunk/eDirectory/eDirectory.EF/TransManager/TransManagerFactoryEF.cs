using eDirectory.Domain.TransManager;

namespace eDirectory.EF.TransManager
{
    public class TransManagerFactoryEF
        : ITransFactory
    {
        public IModelCreator ModelCreator { get; private set; }

        public TransManagerFactoryEF(IModelCreator modelCreator)
        {
            ModelCreator = modelCreator;
        }

        #region Implementation of ITransFactory

        public ITransManager CreateManager()
        {
            return new TransManagerEF(new eDirectoryDbContext(ModelCreator));
        }

        #endregion
    }
}
