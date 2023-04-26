using Application.Common.UnitOfWork;
using Domain.IRepositories;
using Infra.MSSQL;
using Infra.MSSQL.Common;
using Infra.MSSQL.Contexts;
using Infra.MSSQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infra
{
    public static class IoC
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            AddRepositories(services);
            AddSqlServer(services);
            return services;
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void AddSqlServer(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionStringFactory, ConnectionStringFactory>();

            var serviceProvider = services.BuildServiceProvider();
            var connectionString = serviceProvider
                .GetRequiredService<IConnectionStringFactory>()
                .GetConnectionString();

            Console.WriteLine($"Connection String: {connectionString}");

            services.AddDbContextPool<DatabaseContext>(opt => opt.UseSqlServer(connectionString));
            services.AddScoped<IScopedDatabaseContext, ScopedDatabaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
