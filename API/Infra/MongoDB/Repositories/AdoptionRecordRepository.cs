using Domain.Common.Environment;
using Domain.Documents;
using Domain.IRepositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infra.MongoDB.Repositories
{
    public class AdoptionRecordRepository : Collections, IAdoptionRecordRepository
    {
        public AdoptionRecordRepository(IMongoContext context) : base(context)
        {
        }

        public async Task Create(AdoptionRecord record)
        {
            await AdoptionRecords.InsertOneAsync(record);
        }

        public async Task<AdoptionRecord> Get(Expression<Func<AdoptionRecord, bool>> filter)
        {
            return await AdoptionRecords.Find(c => true).FirstOrDefaultAsync();
        }

        public async Task<IList<AdoptionRecord>> GetMany(Expression<Func<AdoptionRecord, bool>> filter)
        {
            return await AdoptionRecords.Find(filter).ToListAsync();
        }
    }
}
