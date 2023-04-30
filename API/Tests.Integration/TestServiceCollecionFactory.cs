using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Infra;
using Domain;
using Application;

namespace Tests.Integration
{
    public static class TestServiceCollecionFactory
    {
        public static IServiceCollection BuildIntegrationTestInfrastructure(string sqlConnectionString, string mongoConnectionString)
        {
            var services = new ServiceCollection();
            services.AddSQLDatabase(sqlConnectionString);
            services.AddMongoDatabase(mongoConnectionString);
            services.AddTestLogs();
            services.AddDomain();
            services.AddApplication();
            services.AddInfra();
            services.AddLogging();

            return services;
        }
    }
}
