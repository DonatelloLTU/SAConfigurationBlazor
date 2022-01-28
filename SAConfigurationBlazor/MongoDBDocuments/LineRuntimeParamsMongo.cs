using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Models.Data_Structures;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLibrary.Models.MongoDBDocuments
{
    public class LineRuntimeParamsMongo : ILineRuntimeParams
    {
        [BsonElement("RecipeName")]
        public string RecipeName { get; set; }
        [BsonElement("Basecoat")]
        public string Basecoat { get; set; }
        [BsonElement("TrackingId")]
        public string TrackingId { get; set; }
        [BsonElement("LaneDefinition")]
        public string LaneDefinition { get; set; }

        public LineParameterOverrideMessageMongo LineOverride { get; set; }
    }
}
