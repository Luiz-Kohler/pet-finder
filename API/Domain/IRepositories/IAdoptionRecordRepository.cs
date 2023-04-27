using Domain.Documents;
using System.Linq.Expressions;

namespace Domain.IRepositories
{
    public interface IAdoptionRecordRepository
    {
        Task Create(AdoptionRecord record);
        Task<AdoptionRecord> Get(Expression<Func<AdoptionRecord, bool>> filter);
        Task<IList<AdoptionRecord>> GetMany(Expression<Func<AdoptionRecord, bool>> filter);
    }
}
