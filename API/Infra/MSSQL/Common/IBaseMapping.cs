using Microsoft.EntityFrameworkCore;

namespace Infra.MSSQL.Common
{
    public interface IBaseMapping
    {
        void MapearEntidade(ModelBuilder modelBuilder);
    }
}
