using Domain.Entities;
using Domain.IRepositories;

namespace Infra.MSSQL.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
