using Infra.MongoDB;
using Infra.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Tests.Integration
{
    [Collection("Sequential")]
    [assembly: CollectionBehavior(DisableTestParallelization = true)]
    public abstract partial class IntegrationTestsBase : IDisposable
    {
        private const string SQLConnectionString = "Data Source=localhost,1433;Initial Catalog=pet-finder;Persist Security Info=True;User ID=sa;Password=Admin@123";
        private const string MongoConnectionString = "mongodb://sa:Admin@localhost:27017";

        private static IServiceProvider GetServiceProvider(IServiceCollection serviceCollection)
        {
            var defaultServiceProviderFactory = new DefaultServiceProviderFactory(new ServiceProviderOptions());
            return defaultServiceProviderFactory.CreateServiceProvider(serviceCollection);
        }

        private static IServiceCollection GetServices()
        {
            return TestServiceCollecionFactory.BuildIntegrationTestInfrastructure(
                SQLConnectionString,
                MongoConnectionString
            );
        }

        private readonly Lazy<IServiceProvider> _rootServiceProvider;
        private readonly Lazy<IServiceScope> _serviceScope;
        private readonly Lazy<DatabaseContext> _databaseContext;
        protected readonly IServiceCollection Services;
        protected IServiceScope ServiceScope => _serviceScope.Value;
        protected IServiceProvider ServiceProvider => ServiceScope.ServiceProvider;
        protected DatabaseContext DatabaseContext => _databaseContext.Value;
        protected IServiceProvider RootServiceProvider => _rootServiceProvider.Value;

        static IntegrationTestsBase()
        {
            var serviceProvider = GetServiceProvider(GetServices());
            TestSQLManager.RebuildDatabase(serviceProvider);
        }

        protected IntegrationTestsBase()
        {
            Services = TestServiceCollecionFactory.BuildIntegrationTestInfrastructure(
                SQLConnectionString,
                MongoConnectionString
            );

            TestSQLManager.TruncateAllTables(GetServiceProvider(Services));

            _rootServiceProvider = new Lazy<IServiceProvider>(() => GetServiceProvider(Services));
            _serviceScope = new Lazy<IServiceScope>(() => _rootServiceProvider.Value.CreateScope());
            _databaseContext = new Lazy<DatabaseContext>(() => ServiceProvider.GetService<DatabaseContext>());
        }

        protected T Mock<T>() where T : class
        {
            var mock = Substitute.For<T>();
            Services.AddTransient(provider => mock);
            return mock;
        }

        protected T GetService<T>() => ServiceProvider.GetService<T>();

        public void Dispose()
        {
            if (_serviceScope.IsValueCreated)
            {
                _serviceScope.Value.Dispose();
            }
        }
    }

    public partial class IntegrationTestsBase
    {
        protected DbSet<TEntity> GetTable<TEntity>()
            where TEntity : class
        {
            return DatabaseContext!.Set<TEntity>();
        }

        protected IList<TEntity> GetEntities<TEntity>()
            where TEntity : class
        {
            return DatabaseContext!
                .Set<TEntity>()
                .AsNoTracking()
                .IgnoreQueryFilters()
                .ToList();
        }

        protected TEntity InsertOne<TEntity>(TEntity entity)
            where TEntity : class
        {
            var entityEntry = DatabaseContext!.Set<TEntity>().Add(entity);
            DatabaseContext.SaveChanges();
            entityEntry.State = EntityState.Detached;
            return entity;
        }

        protected void InsertMany<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            DatabaseContext!.Set<TEntity>().AddRange(entities);
            DatabaseContext.SaveChanges();
        }
    }
}
