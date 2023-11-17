using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AECTech.Dynamo.MongoDB.Object
{
    /// <summary>
    /// Wrapper class for IMongoDatabase
    /// </summary>
    [IsDesignScriptCompatible]
    public class MongoDBDatabase
    {
        /// <summary>
        /// The name of the MongoDB Database connected
        /// </summary>
        /// <returns name ="Name">Returns the name of the MongoDB that is currently connected to</returns>
        public string Name { get { return DataBaseName; } }

        /// <summary>
        /// All the collection name that resides in the database
        /// </summary>
        /// <returns name ="CollectionNames">All available collection names in the database</returns>
        public List<string> AllCollectionNames { get { return _collectionNames; } }

        /// <summary>
        /// The number of collection
        /// </summary>
        /// <returns name ="Count">The number of collection</returns>
        public int CollectionCount { get { return _collectionNames.Count; } }

        private string DataBaseName { get { return _database?.DatabaseNamespace.DatabaseName; } }
        private List<string> _collectionNames { get { return _database?.ListCollectionNames().ToList(); } }
        private IMongoDatabase _database;
        internal MongoDBDatabase(IMongoDatabase mongoDatabase)
        {
            _database = mongoDatabase;
        }

        /// <summary>
        /// Attempts to find the collection of specific name in the database
        /// </summary>
        /// <param name="CollectionName">The name of the collection</param>
        /// <returns name ="MongoCollection">Return the first collection of specific name found in the database</returns>
        public MongoCollection GetCollectionByName(string CollectionName)
        {
            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>(CollectionName);
            return new MongoCollection(collection);
        }

        /// <summary>
        /// Returns all the collection in the database
        /// </summary>
        /// <param name="ToDictionary">To convert the collection to readable data</param>
        /// <returns name="MongoCollections">The collections in the database</returns>
        [NodeCategory("Actions")]
        public IList GetAllCollection(bool ToDictionary = false)
        {
            List<dynamic> result = new List<dynamic>();
            var collectionNames = AllCollectionNames;
            foreach (string collectionName in collectionNames)
            {
                var collection = GetCollectionByName(collectionName);
                if (ToDictionary)
                {
                    result.Add(collection.GetAllDocuments);
                }
                else
                {
                    result.Add(collection);
                }
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_database == null) { return "null"; }

            string basestring = "IMongoDatabase(DatabaseName={0})";
            return string.Format(basestring, DataBaseName);
        }
    }
}
