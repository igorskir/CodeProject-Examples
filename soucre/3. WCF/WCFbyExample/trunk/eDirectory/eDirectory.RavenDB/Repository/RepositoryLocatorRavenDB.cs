using System.Threading;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using eDirectory.Domain.Repository;
using eDirectory.RavenDB.Indexes;

namespace eDirectory.RavenDB.Repository
{
    public class RepositoryLocatorRavenDB
        : RepositoryLocatorBase, IResetable, IStoreInitialiser

    {
        private readonly IDocumentSession _sessionInstance;

        public RepositoryLocatorRavenDB(IDocumentSession session)
        {
            _sessionInstance = session;
        }

        #region Overrides of RepositoryLocatorBase

        protected override IRepository<T> CreateRepository<T>()
        {
            return new RepositoryRavenDB<T>(_sessionInstance);
        }

        #endregion

        #region Implementation of IResetable

        public void Reset()
        {
            var documentStore = _sessionInstance.Advanced.DocumentStore as EmbeddableDocumentStore;
            if (documentStore == null) return;
            while (documentStore.DatabaseCommands.GetStatistics().StaleIndexes.Length != 0)
            {
                Thread.Sleep(10);
            }
            _sessionInstance.Advanced.DatabaseCommands.DeleteByIndex(AllDocumentsById.Name, new IndexQuery());
        }

        #endregion

        #region Implementation of IStoreInitialiser

        public void ConfigureStore()
        {
            var documentStore = _sessionInstance.Advanced.DocumentStore as EmbeddableDocumentStore;
            if (documentStore == null) return;
            IndexCreation.CreateIndexes(typeof(AllDocumentsById).Assembly, documentStore);
        }

        #endregion
    }
}