using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace AECTech.Dynamo.MongoDB.Object
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateDefinition
    {
        /// <summary>
        /// Creates a new update definition
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static UpdateDefinition Create(string Field, object Value)
        {
            return new UpdateDefinition(DefinitionBuilder.Set(Field, Value), Field, Value);
        }

        #region Hidden from dynamo
        private static UpdateDefinitionBuilder<BsonDocument> DefinitionBuilder { get { return Builders<BsonDocument>.Update; } }
        private string Field { get; }
        private object Value { get; }
        internal UpdateDefinition<BsonDocument> InternalUpdateDefinition { get; private set; }
        internal UpdateDefinition(UpdateDefinition<BsonDocument> updateDefinition, string Field, object Value)
        {
            InternalUpdateDefinition = updateDefinition;
            this.Field = Field;
            this.Value = Value;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string basestring = "UpdateDefinition(Field = {0}, Value = {1})";
            return string.Format(basestring, this.Field, this.Value);
        }
        #endregion
    }
}
