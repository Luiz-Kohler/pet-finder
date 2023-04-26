using Domain.Common.Filter;
using Domain.Entities;
using Domain.IRepositories;
using Infra.MSSQL.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infra.MSSQL.Repositories
{
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IList<Pet>> SelectMany(PetFilter filter)
        {
            var query = Entity.Include(x => x.NewOwner)
                                .ThenInclude(x => x.Address)
                              .Include(x => x.OldOwner)
                                .ThenInclude(x => x.Address)
                              .Include(x => x.Images)
                              .AsQueryable();

            query = query.FilterWhen(filter.NewOwnerId is not null, x => x.NewOwnerId == filter.NewOwnerId)
                         .FilterWhen(filter.OldOwnerId is not null, x => x.OldOwnerId == filter.OldOwnerId)
                         .FilterWhen(filter.Id is not null, x => x.Id == filter.Id)
                         .FilterWhen(filter.Size is not null, x => x.Size == filter.Size)
                         .FilterWhen(filter.Type is not null, x => x.Type == filter.Type)
                         .FilterWhen(filter.WasAlreadyAdopted is not null, x => x.NewOwner is not null)
                         .FilterWhen(filter.State is not null, x => x.OldOwner.Address.State.ToLower() == filter.State.ToLower())
                         .FilterWhen(filter.City is not null, x => x.OldOwner.Address.City.ToLower() == filter.City.ToLower());

            return query.ToList();
        }
    }
}
