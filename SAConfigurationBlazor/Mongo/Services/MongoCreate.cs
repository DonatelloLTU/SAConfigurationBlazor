//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MongoDB.Bson;
//using MongoDB.Driver;

//namespace DataAccessLibrary.Mongo.Services
//{
//    public class MongoCreate
//    {
//        #region Variables
//        private MongoClient _client;
//        private IMongoDatabase _database;
//        private MongoDBParams _params;
//        #endregion

//        #region Constructor
//        public MongoCreate()
//        {
//            _params = new MongoDBParams();
//            _client = new MongoClient();
//        }
//        #endregion

//        public void CreateDocumentForCollection<T>(T document, IMongoDBParams param)
//        {
//            lock(this)
//            {
//                if (document == null)
//                    return;

//                if(CollectionExists(param))
//                {
//                    _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).InsertOne(document.ToBsonDocument());
//                }
//                else
//                {
//                    CreateCollection(param);
//                    _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).InsertOne(document.ToBsonDocument());
//                }
//            }
//        }

//        public async Task CreateDocumentForCollectionAsync<T>(T document, IMongoDBParams param)
//        {
//            if (document == null)
//                return;

//            if(CollectionExists(param))
//            {
//                await _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).InsertOneAsync(document.ToBsonDocument());  
//            }
//            else
//            {
//                await CreateCollectionAsync(param);
//                await _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).InsertOneAsync(document.ToBsonDocument());
//            }
//        }

//        #region Helper Methods

//        private bool DatabaseExists(IMongoDBParams param)
//        {
//            return _client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString).Contains(param.DatabaseName);
//        }

//        private bool CollectionExists(IMongoDBParams param)
//        {
//            if (DatabaseExists(param))
//            {
//                var options = new ListCollectionNamesOptions { Filter = new BsonDocument("name", param.CollectionName) };
//                return _client.GetDatabase(param.CollectionName).ListCollectionNames(options).Any();
//            }
//            else
//            {
//                return false;
//            }
//        }

//        private void CreateCollection(IMongoDBParams param)
//        {
//            if(!CollectionExists(param))
//            {
//                _client.GetDatabase(param.DatabaseName).CreateCollection(param.CollectionName);
//            }
//            else if (DatabaseExists(param) && CollectionExists(param))
//            {
//                throw new MongoClientException("Collection " + param.CollectionName + " in Database " + param.DatabaseName + " already exsist.");
//            }
//        }

//        public async Task CreateCollectionAsync(IMongoDBParams param)
//        {
//            if (!CollectionExists(param))
//            {
//                await _client.GetDatabase(param.DatabaseName).CreateCollectionAsync(param.CollectionName);
//            }
//            else if (DatabaseExists(param) && CollectionExists(param))
//            {
//                throw new Exception("Collection " + param.CollectionName + " or Database " + param.DatabaseName + " already exsist");
//            }
//        }
//        #endregion
//    }
//}
