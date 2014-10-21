using System.Globalization;
using System.Linq;
using Raven.Client;
using Raven.Client.Embedded;
using eDirectory.Domain.Entities;
using eDirectory.Domain.Repository;

namespace eDirectory.RavenDB.Repository
{
    public class RepositoryRavenDB<TEntity>
        : IRepository<TEntity>
    {
        private readonly IDocumentSession _sessionInstance;
        private readonly string _idPrefix;

        public RepositoryRavenDB(IDocumentSession session)
        {
            _sessionInstance = session;
            _idPrefix = GetPrefix();
        }

        private static string GetPrefix()
        {
            var typeName = typeof (TEntity).Name;
            var flag = typeName.Last().Equals('s');
            return typeName +
                   (flag
                        ? "es/"
                        : "s/");
        }

        #region Implementation of IRepository<TEntity>

        public TEntity Save(TEntity instance)
        {       
            _sessionInstance.Store(instance);
            return instance;
        }

        public void Update(TEntity instance)
        {
        }

        public void Remove(TEntity instance)
        {
            _sessionInstance.Delete(instance);
        }

        public TEntity GetById(long id)
        {
            return _sessionInstance.Load<TEntity>(_idPrefix +  id);
        }

        public IQueryable<TEntity> FindAll()
        {
            return _sessionInstance.Query<TEntity>();
        }

        #endregion
    }
}