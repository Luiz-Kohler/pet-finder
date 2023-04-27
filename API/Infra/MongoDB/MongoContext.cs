using Domain.Common.Environment;
using MongoDB.Driver;

namespace Infra.MongoDB
{
    public class MongoContext : IMongoContext
    {
        public MongoClient MongoClient { get; set; }
        public IMongoDatabase Database { get; set; }

        public MongoContext(IEnvironmentVariables environmentVariables)
        {
            var connectionString = environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.MongoDBConnection);
            try
            {
                var client = new MongoClient(connectionString);
                Database = client.GetDatabase("pet-finder");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
