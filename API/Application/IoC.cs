using Application.Common.AutoMapper;
using Application.Common.Hash;
using Application.Common.JWT;
using Application.Common.UnitOfWork;
using Application.Common.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IRollbackActionsExecuter, RollbackActionsExecuter>();

            services.AddFluentValidation(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMiddlewares();
            services.AddAutoMapper();

            services.AddJwtAuth();
            services.AddHash();
        }

        private static void AddJwtAuth(this IServiceCollection services)
        {
            services.AddSingleton<IJwtHandler, JwtHandler>();
        }

        private static void AddHash(this IServiceCollection services)
        {
            services.AddSingleton<IHashHandler, HashHandler>();
        }

        private static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserMap());
                mc.AddProfile(new AddressMap());
                mc.AddProfile(new PetMap());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private static void AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(IValidator<>);

            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validatorTypes)
            {
                var requestType = validator.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                i.GetGenericTypeDefinition() == typeof(IValidator<>))
                    .Select(i => i.GetGenericArguments()[0])
                    .First();

                var validatorInterface = validatorType
                    .MakeGenericType(requestType);

                services.AddTransient(validatorInterface, validator);
            }
        }
    }
}
