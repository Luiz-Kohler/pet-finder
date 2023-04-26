using Domain.Common.Filter;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IPetRepository : IBaseRepository<Pet>
    {
        Task<IList<Pet>> SelectMany(PetFilter filter);
    }
}
