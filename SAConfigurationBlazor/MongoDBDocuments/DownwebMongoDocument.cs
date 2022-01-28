using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;


namespace DataLibrary.Models.MongoDBDocuments
{
    public class DownwebJson
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        [BsonElement("RunId")]
        public int RunId { get; }

        public List<ProbeData> DownwebDataByProbe { get; set; } = new List<ProbeData>();

        public DownwebJson()
        {
        }

    }

    public class ProbeData
    {
        [BsonElement("ProbeId")]
        public string ProbeId { get; set; }
        [BsonElement("ProbeName")]
        public string ProbeName { get; set; }
        public List<DownwebDataPoint> DownwebDataPoints { get; set; } = new List<DownwebDataPoint>();
        public ProbeData()
        {

        }
    }

    public class DownwebDataPoint : IDownwebDataPoint
    {
        public DateTime MeasureTime { get; set; }
        public long CycleCount { get; set; }
        public double Value { get; set; }
        public double Microns { get; set; }
        public double? Encoder { get; set; }
        public bool Successful { get; set; }

        public DownwebDataPoint()
        {

        }
    }

}