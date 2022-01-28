using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace DataLibrary.Models.MongoDBDocuments
{
    public class SpectrometerInfoMessageMongo : ISpectrometerInfoMessage
    {
        [BsonElement("SpectrometerID")]
        public int SpectrometerID { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Model")]
        public string Model { get; set; }
        [BsonElement("Type")]
        public string Type { get; set; }

        public double[] Wavelengths { get; set; }
    }
}
