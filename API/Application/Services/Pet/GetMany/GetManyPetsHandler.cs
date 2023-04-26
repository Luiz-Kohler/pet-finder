using AutoMapper;
using Domain.Common.Filter;
using Domain.IRepositories;
using MediatR;

namespace Application.Services.Pet.GetMany
{
    public class GetManyPetsHandler : IRequestHandler<GetManyPetsRequest, GetManyPetsResponse>
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public GetManyPetsHandler(
            IPetRepository petRepository,
            IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<GetManyPetsResponse> Handle(GetManyPetsRequest request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<PetFilter>(request);

            var pets = await _petRepository.SelectMany(filter);

            return _mapper.Map<GetManyPetsResponse>(pets);
        }
    }
}
