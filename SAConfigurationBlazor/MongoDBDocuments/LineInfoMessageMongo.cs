//using System;
//using System.Collections.Generic;
//using System.Text;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

//namespace DataLibrary.Models.MongoDBDocuments
//{
//    public class LineInfoMessageMongo : ILineInfoMessage
//    {
//        [BsonElement("LineID")]
//        public int LineID { get; set; }
//        [BsonElement("Name")]
//        public string Name { get; set; }
//        [BsonElement("Type")]
//        public LineTypeEnum Type { get; set; }

//        public SpecMetrixApplicationEnum PrimaryApplication { get; set; }
//        [BsonElement("TraverserID")]
//        public int? TraverserID { get; set; }
//        [BsonElement("Probes")]
//        public ProbeInfoMessageMongo[] Probes { get; set; }

//        public bool AutomationIntegration { get; set; }

//        public bool ManualMode { get; set; }

//        #region object Overrides

//        public override string ToString()
//        {
//            return Name;
//        }

//        #endregion
//    }
//}