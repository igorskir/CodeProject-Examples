using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace eDirectory.RavenDB.Indexes
{
    public class AllDocumentsById : AbstractIndexCreationTask
    {
        public const string Name = "AllDocumentsById";

        #region Overrides of AbstractIndexCreationTask

        /// <summary>
        /// Creates the index definition.
        /// </summary>
        public override IndexDefinition CreateIndexDefinition()
        {
            return new IndexDefinition
            {
                Name = AllDocumentsById.Name,
                Map = "from doc in docs let DocId = doc[\"@metadata\"][\"@id\"] select new {DocId};"                
            };            
        }

        #endregion
    }
}