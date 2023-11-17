using Dynamo.Graph.Nodes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AECTech.Dynamo.MongoDB.Object
{
    /// <summary>
    /// The mongoclient
    /// </summary>
    [IsDesignScriptCompatible]
    public class MongoDBClient
    {
        /// <summary>
        /// All database names
        /// </summary>
        public List<string> DatabaseNames { get { return _client.ListDatabaseNames().ToList(); } }

        /// <summary>
        /// The cluster of the client
        /// </summary>
        public MongoDBCluster Cluster { get { return new MongoDBCluster(_client.Cluster); } }

        private MongoClient _client;
        internal MongoDBClient(MongoClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the database from its name
        /// </summary>
        /// <param name="Name">The name of the database</param>
        /// <returns>The database found</returns>
        public MongoDBDatabase GetDatabase(string Name)
        {
            return new MongoDBDatabase(_client.GetDatabase(Name));
        }
    }

    /// <summary>
    /// The cluster of the client
    /// </summary>
    [IsDesignScriptCompatible]
    public class MongoDBCluster
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get { return _cluster.ClusterId.Value; } }

        /// <summary>
        /// IsDirectConnection
        /// </summary>
        public bool IsDirectConnection { get { return _cluster.Description.IsDirectConnection; } }

        /// <summary>
        /// IsCompatibleWithDriver
        /// </summary>
        public bool IsCompatibleWithDriver { get { return _cluster.Description.IsCompatibleWithDriver; } }

        /// <summary>
        /// State
        /// </summary>
        public string State { get { return _cluster.Description.State.ToString(); } }

        /// <summary>
        /// LogicalSessionTimeout
        /// </summary>
        public TimeSpan? LogicalSessionTimeout { get { return _cluster.Description.LogicalSessionTimeout; } }

        /// <summary>
        /// Type
        /// </summary>
        public string Type { get { return _cluster.Description.Type.ToString(); } }

        private ICluster _cluster;
        internal MongoDBCluster(ICluster cluster)
        {
            _cluster = cluster;            
        }
    }
}
