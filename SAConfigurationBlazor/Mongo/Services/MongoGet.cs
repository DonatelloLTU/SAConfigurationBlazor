using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLibrary.Mongo.Services
{
    public class MongoGet
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private MongoDBParams _params;

        #region Constructor
        public MongoGet()
        {
            _params = new MongoDBParams();
            _client = new MongoClient();
            //_database = _client.GetDatabase(_params.DatabaseName);
        }
        #endregion


        public BsonDocument GetSpecificDocument(string id, IMongoDBParams param)
        {
            lock(this)
            {
                if(param is null)
                    throw new ArgumentNullException(nameof(param), " cannot be null.");

                if(!CollectionExists(param))
                    throw new MongoClientException($"Unable to locate collection '{param.CollectionName}' for Run: {id}");

                var filter = new BsonDocument("RunID", id);
                return _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(filter).FirstOrDefault();
            }
        }

        public async Task<BsonDocument> GetSpecificDocumentAsync(string id, IMongoDBParams param)
        {
            if (param is null)
                throw new ArgumentNullException(nameof(param), " cannot be null.");

            if (!CollectionExists(param))
                throw new MongoClientException($"Unable to locate collection '{param.CollectionName}' for Run: {id}");

            var filter = new BsonDocument("RunID", id);
            return await _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(filter).FirstOrDefaultAsync();

        }

        public List<BsonDocument> GetCollectionDocumentList(IMongoDBParams param)
        {
            lock (this)
            {
                if(param is null)
                    throw new ArgumentNullException(nameof(param), " cannot be null.");

                if(CollectionExists(param))
                {
                    return _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(doc => true).ToList();
                }
                else
                {
                    return null; 
                }
            }
        }

        public async Task<List<BsonDocument>> GetCollectionDocumentListAsync(IMongoDBParams param)
        {
            if (param is null)
                throw new ArgumentNullException(nameof(param), " cannot be null.");

            if (!CollectionExists(param))
                return null;

            return await _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(doc => true).ToListAsync();
        }


        #region Helper Methods

        private bool DatabaseExists(IMongoDBParams param)
        {
            return _client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString).Contains(param.DatabaseName);
        }

        private bool CollectionExists(IMongoDBParams param)
        {
            if(DatabaseExists(param))
            {
                var options = new ListCollectionNamesOptions { Filter = new BsonDocument("name", param.CollectionName)};
                return _client.GetDatabase(param.CollectionName).ListCollectionNames(options).Any(); 
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
