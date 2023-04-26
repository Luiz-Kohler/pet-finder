using Domain.Common;
using Domain.IRepositories;
using Infra.MSSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infra.MSSQL.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
            where TEntity : BaseEntity
    {
        protected readonly DatabaseContext Context;
        protected readonly DbSet<TEntity> Entity;

        protected BaseRepository(DatabaseContext context)
        {
            Context = context;
            Entity = Context.Set<TEntity>();
        }


        public async Task SaveChanges() => await Context.SaveChangesAsync();

        public Task Update(TEntity entidade)
        {
            Entity.Update(entidade);
            return Task.CompletedTask;
        }

        public Task Update(IEnumerable<TEntity> entidades)
        {
            Entity.UpdateRange(entidades);
            return Task.CompletedTask;
        }

        public Task Delete(TEntity entidade)
        {
            entidade.IsActive = false;
            return Update(entidade);
        }

        public async Task Delete(IEnumerable<TEntity> entidades)
        {
            await Update(entidades.Select(entidade =>
            {
                entidade.IsActive = false; ;
                return entidade;
            }));
        }

        public async Task Create(TEntity entidade)
        {
            await Entity.AddAsync(entidade);
        }

        public async Task Create(IEnumerable<TEntity> entidades)
        {
            await Entity.AddRangeAsync(entidades);
        }

        public async Task<TEntity> SelectOne(Expression<Func<TEntity, bool>> filter)
        {
            return await Entity.IncludeAll().FirstOrDefaultAsync(filter);
        }

        public async Task<IList<TEntity>> SelectMany(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter is null)
                return await Entity.IncludeAll().ToListAsync();

            return await Entity.IncludeAll().Where(filter).ToListAsync();
        }
    }
}
