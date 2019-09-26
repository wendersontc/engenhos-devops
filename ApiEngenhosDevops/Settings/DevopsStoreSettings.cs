namespace ApiEngenhosDevops.Settings
{
    public class DevopsStoreSettings : IBookstoreDatabaseSettings
    {
       public string WorkItensCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IBookstoreDatabaseSettings
    {
        string WorkItensCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}