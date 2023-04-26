using Domain.Entities;
using Domain.IRepositories;

namespace Infra.MSSQL.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        public ImageRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
