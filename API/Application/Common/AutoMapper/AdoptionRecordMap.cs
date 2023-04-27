using AutoMapper;
using Domain.Documents;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class AdoptionRecordMap : Profile
    {
        public AdoptionRecordMap()
        {
            CreateMap<User, Owner>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address));

            CreateMap<Address, OwnerAddress>();

            CreateMap<Pet, AdoptedPet>();
        }
    }
}
