using Domain.Common.Environment;

namespace Infra.MSSQL.Common
{
    public class ConnectionStringFactory : IConnectionStringFactory
    {
        private readonly IEnvironmentVariables _environmentVariables;

        public ConnectionStringFactory(IEnvironmentVariables environmentVariables)
        {
            _environmentVariables = environmentVariables;
        }

        public string GetConnectionString()
        {
            return _environmentVariables.GetEnvironmentVariable(EnvironmentVariablesNames.DBConnection) 
                ?? "Data Source=localhost,1433;Initial Catalog=pet-finder;Persist Security Info=True;User ID=sa;Password=Admin@123";
        }

        //DOCKER
         //"Data Source = sqldata,1433; Initial Catalog = master; Persist Security Info = True; User ID = sa; Password = Admin@123"

        //LOCAL
         //"Data Source=localhost,1433;Initial Catalog=pet-finder;Persist Security Info=True;User ID=sa;Password=Admin@123"
    }
}
