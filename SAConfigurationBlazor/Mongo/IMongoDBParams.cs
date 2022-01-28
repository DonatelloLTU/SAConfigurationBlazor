namespace DataAccessLibrary.Mongo
{
    public interface IMongoDBParams : IDataAccessParams
    {
        //string ConnectionString { get; set; }

        string CollectionName { get; set; }

        //string CrosswebCollectionName { get; set; }
        string DatabaseName { get; set; }
        //string DownwebCollectionName { get; set; }
        //string RegionCollectionName { get; set; }
        //string RunCollectionName { get; set; }

    }
}