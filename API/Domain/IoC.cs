using Domain.Common.Environment;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class IoC
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IEnvironmentVariables, EnvironmentVariables>();
            return services;
        }
    }
}
