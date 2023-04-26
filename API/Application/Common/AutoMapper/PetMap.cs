using Application.Services.Pet.Create;
using Application.Services.Pet.GetMany;
using AutoMapper;
using Domain.Common.Filter;
using Domain.Entities;

namespace Application.Common.AutoMapper
{
    public class PetMap : Profile
    {
        public PetMap()
        {
            CreateMap<CreatePetRequest, Pet>()
                .ForMember(x => x.IsActive, opt => opt.MapFrom(x => true))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => new List<Image>()));

            CreateMap<GetManyPetsRequest, PetFilter>();

            CreateMap<List<Pet>, GetManyPetsResponse>()
                .ForMember(x => x.Pets, opt => opt.MapFrom(x => x != null ? x : new()));

            CreateMap<Pet, PetResponse>()
                .ForMember(x => x.OldOwner, opt => opt.MapFrom(x => x.OldOwner))
                .ForMember(x => x.NewOwner, opt => opt.MapFrom(x => x.NewOwner))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images));

            CreateMap<Address, AddressForPetResponse>();

            CreateMap<User, UserForPetResponse>()
                .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address));

            CreateMap<Image, ImageForPetResponse>();
        }
    }
}
