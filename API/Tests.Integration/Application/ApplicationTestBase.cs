using Application.Common.UnitOfWork;
using Infra.MongoDB;
using Infra.MSSQL;
using Infra.MSSQL.Common;
using Infra.MSSQL.Contexts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Tests.Integration.Application
{
    [Collection("Sequential")]
    [assembly: CollectionBehavior(DisableTestParallelization = true)]
    public abstract class ApplicationTestBase : IntegrationTestsBase
    {
        [DebuggerStepThrough]
        protected Task Handle<TRequest>(TRequest request)
            where TRequest : IRequest<Unit>
        {
            return Handle<TRequest, Unit>(request);
        }

        [DebuggerStepThrough]
        protected async Task<TResponse> Handle<TRequest, TResponse>(TRequest request)
            where TRequest : IRequest<TResponse>
        {
            var mediator = ServiceProvider.GetService<IMediator>();
            return await mediator!.Send(request, CancellationToken.None);
        }

        protected (DatabaseContext, IUnitOfWork) GetNewUnitOfWork()
        {
            var scopedDatabaseContext = ServiceProvider.GetService<IScopedDatabaseContext>();
            var unitOfWork = new UnitOfWork(scopedDatabaseContext);

            return (scopedDatabaseContext!.Context, unitOfWork);
        }
    }
}
