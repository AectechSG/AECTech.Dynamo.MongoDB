using Autodesk.DesignScript.Runtime;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace AECTech.Dynamo.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    [SupressImportIntoVM]
    public static class MongoDBHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bson"></param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> ToDynamicDictionary(this BsonDocument bson)
        {
            Dictionary<string, dynamic> dynamicDict = new Dictionary<string, dynamic>();
            foreach (var elm in bson.Elements)
            {
                if (elm.Name != "_id")
                    if (!dynamicDict.ContainsKey(elm.Name))
                        if (!elm.Value.IsBsonDocument)
                            dynamicDict.Add(elm.Name, elm.Value);
                        else
                            dynamicDict.Add(elm.Name, (elm.Value as BsonDocument).ToDynamicDictionary());
            }
            return dynamicDict;
        }
    }
}
