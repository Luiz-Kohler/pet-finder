using Domain.Entities;
using Domain.IRepositories;

namespace Infra.MSSQL.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
