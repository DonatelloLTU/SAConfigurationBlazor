using System;
using System.Collections.Generic;
using System.Text;
using SA.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataLibrary.Models.MongoDBDocuments
{
    public class ProbeInfoMessageMongo : IProbeInfoMessage
    {
        [BsonElement("ProbeID")]
        public int ProbeID { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("LineID")]
        public int LineID { get; set; }
        [BsonElement("Axis")]
        public MotionAxisEnum Axis { get; set; }

        public SpectrometerInfoMessageMongo Spectrometer { get; set; }

        public PhysicalProbeType PhysicalProbeType { get; set; }
    }
}
