using Application.Services.Pet.GetMany;
using AutoMapper;
using Domain.Common.Filter;
using Domain.IRepositories;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;

namespace Tests.UnitTests.Services.Pet
{
    public class GetManyPetsTests
    {
        private readonly GetManyPetsHandler _handler;
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;
        public GetManyPetsTests()
        {
            _petRepository = Substitute.For<IPetRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new GetManyPetsHandler(_petRepository, _mapper);
        }

        [Fact]
        public async Task Should_Get_Many_Pets()
        {
            var request = Builder<GetManyPetsRequest>.CreateNew().Build();
            var filter = Builder<PetFilter>.CreateNew().Build();
            var pets = Builder<Domain.Entities.Pet>.CreateListOfSize(3).Build();
            var petResponse = Builder<GetManyPetsResponse>.CreateNew()
                .With(x => x.Pets, Builder<PetResponse>.CreateListOfSize(pets.Count).Build())
                .Build();

            _mapper.Map<PetFilter>(Arg.Any<GetManyPetsRequest>()).Returns(filter);
            _petRepository.SelectMany(Arg.Any<PetFilter>()).Returns(pets);
            _mapper.Map<GetManyPetsResponse>(Arg.Any<List<Domain.Entities.Pet>>()).Returns(petResponse);

            var response = await _handler.Handle(request, new CancellationToken());

            _mapper.Received(1).Map<PetFilter>(Arg.Any<GetManyPetsRequest>());
            await _petRepository.Received(1).SelectMany(Arg.Any<PetFilter>());
            _mapper.Received(1).Map<GetManyPetsResponse>(Arg.Any<List<Domain.Entities.Pet>>());
            response.Should().NotBeNull();
            response.Pets.Count().Should().Be(pets.Count);
        }
    }
}