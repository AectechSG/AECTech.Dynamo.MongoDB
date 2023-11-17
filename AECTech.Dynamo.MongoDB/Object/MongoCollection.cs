using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AECTech.Dynamo.MongoDB.Object
{
    /// <summary>
    /// Wrapper class for IMongoCollection
    /// </summary>
    public class MongoCollection
    {
        /// <summary>
        /// Returns the collection name
        /// </summary>
        public string Name { get { return _collectionName; } }
        /// <summary>
        /// Returns all document in the collection
        /// </summary>
        public IEnumerable GetAllDocuments
        {
            get { return AllDocuments.Select(x => x.ToDynamicDictionary()); }

        }
        /// <summary>
        /// Get the documents by a specific filter
        /// </summary>
        /// <param name="FilterDefinition"></param>
        /// <returns></returns>
        [NodeCategory("Query")]
        public IEnumerable GetDocumentsByFilter(
            [DefaultArgument("AECTech.Dynamo.MongoDB.Object.FilterDefinition.Empty()")] FilterDefinition FilterDefinition)
        {
            if (FilterDefinition == null) FilterDefinition = FilterDefinition.Empty();
            FilterDefinition<BsonDocument> filter = FilterDefinition.InternalFilterDefinition;
            return InternalMongoCollection.Find(filter).ToList().Select(x => x.ToDynamicDictionary());
        }

        /// <summary>
        /// Update a single document in the collection
        /// </summary>
        /// <param name="FilterDefinition"></param>
        /// <param name="UpdateDefinition"></param>
        /// <returns></returns>
        public bool UpdateOneDocument(FilterDefinition FilterDefinition, UpdateDefinition UpdateDefinition)
        {
            UpdateResult result = InternalMongoCollection.UpdateOne(FilterDefinition.InternalFilterDefinition, UpdateDefinition.InternalUpdateDefinition);
            return result.IsAcknowledged;
        }

        /// <summary>
        /// Update a many document in the collection
        /// </summary>
        /// <param name="FilterDefinition"></param>
        /// <param name="UpdateDefinition"></param>
        /// <returns></returns>
        public bool UpdateManyDocuments(FilterDefinition FilterDefinition, UpdateDefinition UpdateDefinition)
        {
            UpdateResult result = InternalMongoCollection.UpdateMany(FilterDefinition.InternalFilterDefinition, UpdateDefinition.InternalUpdateDefinition);
            return result.IsAcknowledged;
        }

        /// <summary>
        /// Insert a document into the collection
        /// </summary>
        /// <param name="Document"></param>
        public void InsertOneDocument(IDictionary Document)
        {
            var bsondoc = Document.ToBsonDocument();
            InternalMongoCollection.InsertOne(bsondoc);
        }

        /// <summary>
        /// Insert many document into the collection
        /// </summary>
        /// <param name="Documents"></param>
        public void InsertManyDocument(IEnumerable<IDictionary> Documents)
        {
            var bsondocs = Documents.Select(d => d.ToBsonDocument());
            InternalMongoCollection.InsertMany(bsondocs);
        }

        /// <summary>
        /// Delete a document in the collection
        /// </summary>
        /// <param name="FilterDefinition"></param>
        [MultiReturn(new[] { "Success", "DeleteCount" })]
        public Dictionary<string, object> DeleteOneDocument(FilterDefinition FilterDefinition)
        {
            var result = InternalMongoCollection.DeleteOne(FilterDefinition.InternalFilterDefinition);
            bool isDeleted = result.IsAcknowledged;
            if (isDeleted)
            {
                return new Dictionary<string, object>
                {
                    { "Success", isDeleted },
                    { "DeleteCount", result.DeletedCount }
                };
            }
            else
            {
                return new Dictionary<string, object>
                {
                    { "Success", isDeleted },
                    { "DeleteCount", null }
                };
            }
        }

        /// <summary>
        /// Delete many document in the collection
        /// </summary>
        /// <param name="FilterDefinition"></param>
        [MultiReturn(new[] { "Success", "DeleteCount" })]
        public Dictionary<string, object> DeleteManyDocument(FilterDefinition FilterDefinition)
        {
            var result = InternalMongoCollection.DeleteMany(FilterDefinition.InternalFilterDefinition);
            bool isDeleted = result.IsAcknowledged;
            if (isDeleted)
            {
                return new Dictionary<string, object>
                {
                    { "Success", isDeleted },
                    { "DeleteCount", result.DeletedCount }
                };
            }
            else
            {
                return new Dictionary<string, object>
                {
                    { "Success", isDeleted },
                    { "DeleteCount", null }
                };
            }
        }

        #region Hidden from dynamo
        internal IMongoCollection<BsonDocument> InternalMongoCollection { get; private set; }
        private string _collectionName { get { return InternalMongoCollection.CollectionNamespace.CollectionName; } }
        private int DocumentCount { get { return (int)InternalMongoCollection.CountDocuments(new BsonDocument()); } }
        internal MongoCollection(IMongoCollection<BsonDocument> mongoCollection)
        {
            this.InternalMongoCollection = mongoCollection;
        }
        internal List<BsonDocument> AllDocuments
        {
            get { return InternalMongoCollection.Find(new BsonDocument()).ToList(); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string basestring = "IMongoCollection<BsonDocument>(CollectionName={0}, DocumentCount={1})";
            return string.Format(basestring, Name, DocumentCount);
        }
        #endregion
    }
}
