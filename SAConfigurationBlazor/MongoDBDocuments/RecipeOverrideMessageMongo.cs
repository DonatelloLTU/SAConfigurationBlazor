using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLibrary.Models.MongoDBDocuments
{
    public class RecipeOverrideMessageMongo : IRecipeOverrideMessage
    {
        /// <summary>
        /// Layer_ID that is getting adjusted.
        /// </summary>
        [BsonElement("Layer_ID")]
        public int Layer_ID { get; set; }

        /// <summary>
        /// Starting Integration Time, or defined Integration Time if AutoIT is Off
        /// </summary>
        public int? Integration_Time { get; set; }

        public double? Target_Spec { get; set; }

        public double? Graph_Top { get; set; }

        public double? Graph_Bottom { get; set; }

        public double? Limit_Upper { get; set; }

        public double? Limit_Lower { get; set; }

    }
}
