using AECTech.Dynamo.MongoDB.Object;
using Dynamo.Graph.Nodes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AECTech.Dynamo.MongoDB
{
    /// <summary>
    /// Class that stores all method regarding MongoDB
    /// </summary>
    [IsDesignScriptCompatible]
    public static class MongoDB
    {
        /// <summary>
        /// Returns the current connected MongoDB client
        /// </summary>
        /// <returns name ="MongoDatabase">Returns the MongoDB that is currently connected to</returns>
        [NodeCategory("Create")]
        public static MongoDBClient ByConnectionString(string ConnectionString)
        {
            var settings = MongoClientSettings.FromConnectionString(ConnectionString);
            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            // Create a new client and connect to the server
            var client = new MongoClient(settings);
            var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

            return new MongoDBClient(client);
        }        
    }
}
