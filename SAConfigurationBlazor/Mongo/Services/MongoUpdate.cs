using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Mongo.Services
{
    public class MongoUpdate
    {
        #region Variables
        private MongoClient _client;
        private IMongoDatabase _database;
        private MongoDBParams _params;
        #endregion

        #region Constructor
        public MongoUpdate()
        {
            _params = new MongoDBParams();
            _client = new MongoClient();
        }
        #endregion

        public void UpdateSpecificDocument<T>(string id, T document, IMongoDBParams param)
        {
            if (CollectionExists(param))
            {
                
                var filter = new BsonDocument("RunID", id);
                var result = _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(filter).FirstOrDefault();
                _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).ReplaceOne(result, document.ToBsonDocument());
            }
        }

        public async Task UpdateSpecificDocumentAsync<T>(string id, T document, IMongoDBParams param)
        {
            if (CollectionExists(param))
            {
                var filter = new BsonDocument("RunID", id);
                var result = await _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(filter).FirstOrDefaultAsync();
                _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).ReplaceOne(result, document.ToBsonDocument());
            }
        }
        #region Helper Methods

        private bool DatabaseExists(IMongoDBParams param)
        {
            return _client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString).Contains(param.DatabaseName);
        }

        private bool CollectionExists(IMongoDBParams param)
        {
            if (DatabaseExists(param))
            {
                var options = new ListCollectionNamesOptions { Filter = new BsonDocument("name", param.CollectionName) };
                return _client.GetDatabase(param.CollectionName).ListCollectionNames(options).Any();
            }
            else
            {
                return false;
            }
        }

        private void CreateCollection(IMongoDBParams param)
        {
            if (!CollectionExists(param))
            {
                _client.GetDatabase(param.DatabaseName).CreateCollection(param.CollectionName);
            }
            else if (DatabaseExists(param) && CollectionExists(param))
            {
                throw new MongoClientException("Collection " + param.CollectionName + " in Database " + param.DatabaseName + " already exsist.");
            }
        }

        public async Task CreateCollectionAsync(IMongoDBParams param)
        {
            if (!CollectionExists(param))
            {
                await _client.GetDatabase(param.DatabaseName).CreateCollectionAsync(param.CollectionName);
            }
            else if (DatabaseExists(param) && CollectionExists(param))
            {
                throw new Exception("Collection " + param.CollectionName + " or Database " + param.DatabaseName + " already exsist");
            }
        }
        #endregion
    }
}
