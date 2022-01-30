
//using DataLibrary.Models;
//using DataLibrary.Models.JSON_Structures;
//using DataLibrary.Models.MongoDBDocuments;
//using MongoDB.Bson;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccessLibrary.Mongo.Services
//{
//    public class MongoDataService
//    {
//        #region **************Testing**************
//        //private readonly IMongoCollection<RunInfoJson> _runInfoJson;//Full file as currently JSON
//        //private readonly IMongoCollection<RunLayerMessage> _runLayerMessage; //Full file as currently JSON
//        //private readonly IMongoCollection<RecipeMessage> _recipeMessage;//Full file as currently JSON
//        //private readonly IMongoCollection<CrosswebDocument> _crosswebData;
//        //private readonly IMongoCollection<DownwebDocument> _downwebData;
//        //private readonly IMongoCollection<RegionDocument> _regionData;
//        //private readonly IMongoCollection<RunInfoDocument> _runInfo;
//        #endregion

//        #region Variables
//        private MongoClient _client;
//        private IMongoDatabase _database;

//        private MongoDBParams _params;
//        #endregion

//        #region Constructor
//        public MongoDataService()
//        {
//            _params = new MongoDBParams();
//            _client = new MongoClient();
//            _database = _client.GetDatabase(_params.DatabaseName);

//            #region **************Testing**************
//            //_runInfoJson = _database.GetCollection<RunInfoJson>(_params.RunInfoFullCollection);//Smae as current JSON implementation
//            //_runLayerMessage = _database.GetCollection<RunLayerMessage>(_params.RunLayerMessage);//Same as current JSON implementation
//            //_recipeMessage = _database.GetCollection<RecipeMessage>(_params.RecipeMessage);//Same as current JSON implementation
//            #endregion
//        }
//        #endregion

//        #region Helper Methods
//        /// <summary>
//        /// Check if database exsist in MongoDB
//        /// </summary>
//        /// <param name="databaseName"></param>
//        /// <returns></returns>
//        private bool DatabaseExists(IMongoDBParams param)
//        {
//            return _client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString).Contains(param.DatabaseName);
//        }

//        /// <summary>
//        /// Check if collection exists in specific database
//        /// </summary>
//        /// <param name="databaseName"></param>
//        /// <param name="collectionName"></param>
//        /// <returns></returns>
//        private bool CollectionExists(IMongoDBParams param)
//        {

//            if (DatabaseExists(param))
//            {
//                var filter = new BsonDocument("name", param.CollectionName);
//                var options = new ListCollectionNamesOptions { Filter = filter };
//                return _client.GetDatabase(param.CollectionName).ListCollectionNames(options).Any();
//            }
//            else
//            {
//                return false;
//            }
//        }

//        /// <summary>
//        /// Create specified dataase
//        /// </summary>
//        /// <param name="databaseName"></param>
//        public void CreateEmptyDatabase(IMongoDBParams param)
//        {
//            if (!DatabaseExists(param))
//            {
//                _client.GetDatabase(param.DatabaseName).CreateCollection("SA");
//            }
//        }

//        public async Task CreateEmptyDatabaseAsync(IMongoDBParams param)
//        {
//            if(!DatabaseExists(param))
//            {
//                await _client.GetDatabase(param.DatabaseName).CreateCollectionAsync("SA");
//            }
//        }

//        /// <summary>
//        /// Creates collection within specific database
//        /// </summary>
//        /// <exception cref="Exception"></exception>
//        public void CreateCollection(IMongoDBParams param)
//        {
//            if (!CollectionExists(param))
//            {
//                _client.GetDatabase(param.DatabaseName).CreateCollection(param.CollectionName);
//            }
//            else if (DatabaseExists(param) && CollectionExists(param))
//            {
//                throw new Exception("Collection " + param.CollectionName + " already exsist.");
//            }
//        }

//        public async Task CreateCollectionAsync(IMongoDBParams param)
//        {
//            if(!CollectionExists(param))
//            {
//                await _client.GetDatabase(param.DatabaseName).CreateCollectionAsync(param.CollectionName);
//            }
//            else if (DatabaseExists(param) && CollectionExists(param))
//            {
//                throw new Exception("Collection " + param.CollectionName + " or Database " + param.DatabaseName + " already exsist");
//            }
//        }

//        /// <summary>
//        /// Delete collection from specific database
//        /// </summary>
//        /// <exception cref="Exception"></exception>
//        public void DeleteCollection(IMongoDBParams param)
//        {
//            if (!DatabaseExists(param))
//            {
//                throw new Exception(param.DatabaseName + " does not exist!");
//            }
//            else if (!CollectionExists(param))
//            {
//                throw new Exception(param.CollectionName + " collection, does not exist");
//            }
//            else
//            {
//                _client.GetDatabase(param.DatabaseName).DropCollection(param.CollectionName);
//            }
//        }

//        public async Task DeleteCollectionAsync(IMongoDBParams param)
//        {
//            if (!DatabaseExists(param))
//            {
//                throw new Exception(param.DatabaseName + "database does not exist!");
//            }
//            else if (!CollectionExists(param))
//            {
//                throw new Exception(param.CollectionName + " collection, does not exist");
//            }
//            else
//            {
//                await _client.GetDatabase(param.DatabaseName).DropCollectionAsync(param.CollectionName);
//            }
//        }

//        /// <summary>
//        /// Delete specific database
//        /// </summary>
//        /// <exception cref="Exception"></exception>
//        public void DeleteDatabase(IMongoDBParams param)
//        {
//            if (DatabaseExists(param))
//            {
//                _client.DropDatabase(param.DatabaseName);
//            }
//            else
//            {
//                throw new Exception(param.DatabaseName + " database does not exist.");
//            }
//        }

//        public async Task DeleteDatabaseAsync(IMongoDBParams param)
//        {
//            if(DatabaseExists(param))
//            {
//                await _client.DropDatabaseAsync(param.DatabaseName);
//            }
//            else
//            {
//                throw new Exception(param.DatabaseName + " database does not exist.");
//            }
//        }

//        /// <summary>
//        /// Check does the document exist in specified collection
//        /// </summary>
//        /// <param name="documentID"></param>
//        /// <param name="param"></param>l
//        /// <returns></returns>
//        public bool DocumentExistInCollection(string documentID,IMongoDBParams param)
//        {
//            var filter = new BsonDocument("RunID", documentID);
//            return _client.GetDatabase(param.DatabaseName).GetCollection<BsonDocument>(param.CollectionName).Find(filter).Any();

//        }


//        #endregion


//    }
//}
