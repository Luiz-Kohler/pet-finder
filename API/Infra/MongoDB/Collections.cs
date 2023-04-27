using Domain.Documents;
using MongoDB.Driver;

namespace Infra.MongoDB
{
    public class Collections 
    {
        private readonly IMongoContext _context;

        public Collections(IMongoContext context)
        {
            _context = context;
        }

        public IMongoCollection<AdoptionRecord> AdoptionRecords => _context.Database.GetCollection<AdoptionRecord>("adoption-record");

    }
}
