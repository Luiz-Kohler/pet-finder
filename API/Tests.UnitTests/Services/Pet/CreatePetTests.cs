using Application.Common.Exceptions;
using Application.Services.Pet.Create;
using AutoMapper;
using Domain.IRepositories;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Tests.UnitTests.Services.Pet
{
    public class CreatePetTests
    {
        private readonly CreatePetHandler _handler;
        private readonly IUserRepository _userRepository;
        private readonly IPetRepository _petRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public CreatePetTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _petRepository = Substitute.For<IPetRepository>();
            _imageRepository = Substitute.For<IImageRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new CreatePetHandler(
                _userRepository,
                _petRepository,
                _imageRepository,
                _mapper);
        }

        [Fact]
        public async Task Should_Create_Pet()
        {
            var owner = Builder<Domain.Entities.User>.CreateNew().Build();
            var pet = Builder<Domain.Entities.Pet>.CreateNew().Build();
            var request = Builder<CreatePetRequest>.CreateNew().Build();

            _mapper.Map<Domain.Entities.Pet>(Arg.Any<CreatePetRequest>()).ReturnsForAnyArgs(pet);
            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>()).ReturnsForAnyArgs(owner);

            var response = await _handler.Handle(request, new CancellationToken());

            await _userRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());
            await _petRepository.Received(1).Create(Arg.Any<Domain.Entities.Pet>());
            _mapper.Received(1).Map<Domain.Entities.Pet>(Arg.Any<CreatePetRequest>());
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Return_When_Not_Found_Owner()
        {
            var request = Builder<CreatePetRequest>.CreateNew().Build();

            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>()).ReturnsNullForAnyArgs();

            var act = async () => await _handler.Handle(request, new CancellationToken());

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("User not found with this id.");
            await _userRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());
            await _petRepository.DidNotReceive().Create(Arg.Any<Domain.Entities.Pet>());
            _mapper.DidNotReceive().Map<Domain.Entities.Pet>(Arg.Any<CreatePetRequest>());
        }
    }
}
