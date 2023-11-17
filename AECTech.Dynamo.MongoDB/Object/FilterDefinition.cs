using Autodesk.DesignScript.Runtime;
using Dynamo.Graph.Nodes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AECTech.Dynamo.MongoDB.Object
{    
    /// <summary>
    ///
    /// </summary>
    [IsDesignScriptCompatible]
    public class FilterDefinition
    {
        /// <summary>
        /// Returns the filter type of this filterdefinition
        /// </summary>
        public string FilterType { get; private set; }

        /// <summary>
        /// Builds an Empty filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Empty()
        {
            return new FilterDefinition(DefinitionBuilder.Empty, Object.FilterType.Empty);
        }
        /// <summary>
        /// Builds an EQ filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Eq(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.Eq(Field, Value), Object.FilterType.Eq);
        }
        /// <summary>
        /// Builds an All filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition All(string Field, IEnumerable<object> Values)
        {
            return new FilterDefinition(DefinitionBuilder.All(Field, Values), Object.FilterType.All);
        }
        /// <summary>
        /// Builds an And filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition And(IEnumerable<FilterDefinition> FilterDefinitions)
        {
            return new FilterDefinition(DefinitionBuilder.And(FilterDefinitions.Select(x => x.InternalFilterDefinition)), Object.FilterType.And);
        }
        /// <summary>
        /// Builds an Or filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Or(IEnumerable<FilterDefinition> FilterDefinitions)
        {
            return new FilterDefinition(DefinitionBuilder.Or(FilterDefinitions.Select(x => x.InternalFilterDefinition)), Object.FilterType.Or);
        }
        /// <summary>
        /// Builds an Any equal filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyEq(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.AnyEq(Field, Value), Object.FilterType.AnyEq);
        }
        /// <summary>
        /// Builds an Any Greater Than filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyGt(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.AnyGt(Field, Value), Object.FilterType.AnyGt);
        }
        /// <summary>
        /// Builds an Any Greater Than or Equals to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyGte(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.AnyGte(Field, Value), Object.FilterType.AnyGte);
        }
        /// <summary>
        /// Builds an Any In array filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyIn(string Field, IEnumerable<object> Values)
        {
            return new FilterDefinition(DefinitionBuilder.AnyIn(Field, Values), Object.FilterType.AnyIn);
        }
        /// <summary>
        /// Builds an Any Not In array filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyNin(string Field, IEnumerable<object> Values)
        {
            return new FilterDefinition(DefinitionBuilder.AnyNin(Field, Values), Object.FilterType.AnyNin);
        }
        /// <summary>
        /// Builds an Any Lesser than filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyLt(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.AnyLt(Field, Value), Object.FilterType.AnyLt);
        }
        /// <summary>
        /// Builds an Any Lesser than or Equals to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyLte(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.AnyLte(Field, Value), Object.FilterType.AnyLte);
        }
        /// <summary>
        /// Builds an Any Not Equals to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition AnyNe(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.AnyNe(Field, Value), Object.FilterType.AnyNe);
        }
        /// <summary>
        /// Builds a Not Equals to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Ne(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.Ne(Field, Value), Object.FilterType.Ne);
        }
        ///<summary>
        /// Builds a Greater than filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Gt(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.Gt(Field, Value), Object.FilterType.Gt);
        }
        ///<summary>
        /// Builds a Greater than or Equals to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Gte(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.Gte(Field, Value), Object.FilterType.Gte);
        }
        ///<summary>
        /// Builds a Lesser than or Equals to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Lte(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.Lte(Field, Value), Object.FilterType.Lte);
        }
        ///<summary>
        /// Builds a Lesser than filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Lt(string Field, object Value)
        {
            return new FilterDefinition(DefinitionBuilder.Lt(Field, Value), Object.FilterType.Lt);
        }
        ///<summary>
        /// Builds a Not filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Not(FilterDefinition FilterDefinition)
        {
            return new FilterDefinition(DefinitionBuilder.Not(FilterDefinition.InternalFilterDefinition), Object.FilterType.Not);
        }
        ///<summary>
        /// Builds an In to filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition In(string Field, IEnumerable<object> Values)
        {
            return new FilterDefinition(DefinitionBuilder.In(Field, Values), Object.FilterType.In);
        }
        /// <summary>
        /// Builds a modulo filter definition
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition Mod(string Field, long Modulus, long Remainder)
        {
            return new FilterDefinition(DefinitionBuilder.Mod(Field, Modulus, Remainder), Object.FilterType.Mod);
        }
        /// <summary>
        /// Builds a ElemMatch filter for array field
        /// </summary>
        /// <returns></returns>
        [NodeCategory("Actions")]
        public static FilterDefinition ElemMatch(string Field, string ElementName, object ElementValue)
        {
            BsonDocument bsons = new BsonDocument();
            bsons.AddRange(new Dictionary<string, object>() { { ElementName, ElementValue } });
            return new FilterDefinition(DefinitionBuilder.ElemMatch<BsonDocument>(Field, bsons), Object.FilterType.ElemMatch);
        }

        #region Hidden from dynamo
        private static FilterDefinitionBuilder<BsonDocument> DefinitionBuilder { get { return Builders<BsonDocument>.Filter; } }
        internal FilterDefinition<BsonDocument> InternalFilterDefinition { get; private set; }
        internal FilterDefinition(FilterDefinition<BsonDocument> filterDefinition, FilterType filterType)
        {
            FilterType = System.Enum.GetName(typeof(FilterType), filterType);
            InternalFilterDefinition = filterDefinition;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string basestring = "FilterDefinition.{0}";
            return string.Format(basestring, FilterType);
        }
        #endregion
    }
    [SupressImportIntoVM]
    internal enum FilterType
    {
        Empty = 0,
        All = 1,
        And = 2,
        AnyEq = 3,
        AnyGt = 4,
        AnyGte = 5,
        AnyIn = 6,
        AnyLt = 7,
        AnyLte = 8,
        AnyNe = 9,
        AnyNin = 10,
        Eq = 11,
        Gt = 12,
        Gte = 13,
        In = 14,
        Lt = 15,
        Lte = 16,
        Mod = 17,
        Ne = 18,
        Not = 19,
        Or = 20,
        ElemMatch = 21,
    }
}
