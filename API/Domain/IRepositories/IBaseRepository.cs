using Domain.Common;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task SaveChanges();
        Task Create(TEntity entity);
        Task Create(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
        Task Update(IEnumerable<TEntity> entities);
        Task Delete(TEntity entity);
        Task Delete(IEnumerable<TEntity> entities);
        Task<IList<TEntity>> SelectMany(Expression<Func<TEntity, bool>> filter = null);
        Task<TEntity> SelectOne(Expression<Func<TEntity, bool>> filter);
    }
}
