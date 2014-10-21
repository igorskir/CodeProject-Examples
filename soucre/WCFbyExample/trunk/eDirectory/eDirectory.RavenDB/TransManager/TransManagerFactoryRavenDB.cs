using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using eDirectory.Domain.TransManager;

namespace eDirectory.RavenDB.TransManager
{
    public class TransManagerFactoryRavenDB
        : ITransFactory
    {
        private IDocumentStore _documentStore;

        private IDocumentStore DocumentStore
        {
            get
            {
                if (_documentStore != null) return _documentStore;
                _documentStore = InitialiseDocumentStore();
                return _documentStore;
            }
        }

        private bool IsSetForTesting { get; set; }

        private IDocumentStore InitialiseDocumentStore()
        {
            var store =
                IsSetForTesting
                    ? new EmbeddableDocumentStore
                          {
                              RunInMemory = true
                          }
                    : new EmbeddableDocumentStore {DataDirectory = "eDirectory"};

            store.Initialize();
            return store;
        }

        #region Implementation of ITransFactory

        public ITransManager CreateManager()
        {
            return new TransManagerRavenDB(DocumentStore.OpenSession());
        }

        #endregion
    }
}
