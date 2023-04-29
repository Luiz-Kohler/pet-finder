using Application.Common.Exceptions;
using Application.Services.Pet.Adopt;
using AutoMapper;
using Domain.Documents;
using Domain.IRepositories;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Tests.UnitTests.Services.Pet
{
    public class AdoptHandlerTests
    {
        private readonly AdoptHandler _handler;
        private readonly IUserRepository _userRepository;
        private readonly IPetRepository _petRepository;
        private readonly IAdoptionRecordRepository _adoptionRecordRepository;
        private readonly IMapper _mapper;

        public AdoptHandlerTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _petRepository = Substitute.For<IPetRepository>();
            _adoptionRecordRepository = Substitute.For<IAdoptionRecordRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new AdoptHandler(_userRepository, _petRepository, _adoptionRecordRepository, _mapper);
        }

        [Fact]
        public async Task Should_Adopt_Pet()
        {
            var oldOwnerId = 1;
            var oldOwner = Builder<Domain.Entities.User>.CreateNew()
    .With(x => x.Id, oldOwnerId).Build();
            var newOwnerId = 2;
            var newOwner = Builder<Domain.Entities.User>.CreateNew()
                .With(x => x.Id, newOwnerId).Build();
            var pet = Builder<Domain.Entities.Pet>.CreateNew()
                .With(x => x.OldOwner, oldOwner)
                .With(x => x.OldOwnerId, oldOwnerId)
                .Build();

            var request = new AdoptRequest { PetId = pet.Id, NewOwnerId = newOwnerId };

            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>())
                .ReturnsForAnyArgs(newOwner);

            _petRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>())
                .ReturnsForAnyArgs(pet);

            var response = await _handler.Handle(request, new CancellationToken());

            await _userRepository.Received(1)
                .SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());
            await _petRepository.Received(1)
                .SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>());
            await _petRepository.Received(1)
                .Update(Arg.Any<Domain.Entities.Pet>());
            await _adoptionRecordRepository.Received(1)
                .Create(Arg.Any<AdoptionRecord>());
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Throw_Not_Found_Exception_When_New_Owner_Not_Found()
        {
            var pet = Builder<Domain.Entities.Pet>.CreateNew().Build();
            var newOwnerId = 2;
            var request = new AdoptRequest { PetId = pet.Id, NewOwnerId = newOwnerId };

            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>())
                .ReturnsNullForAnyArgs();

            var act = async () => await _handler.Handle(request, new CancellationToken());

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("User not found.");

            await _userRepository.Received(1)
                .SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());

            await _petRepository.DidNotReceiveWithAnyArgs()
                .SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>());

            await _petRepository.DidNotReceiveWithAnyArgs()
                .Update(Arg.Any<Domain.Entities.Pet>());

            await _adoptionRecordRepository.DidNotReceiveWithAnyArgs()
                .Create(Arg.Any<AdoptionRecord>());
        }

        [Fact]
        public async Task Should_Throw_Validation_Exception_When_Owner_Try_Adopt_His_Pet()
        {
            var oldOwnerId = 1;
            var oldOwner = Builder<Domain.Entities.User>.CreateNew()
                .With(x => x.Id, oldOwnerId).Build();
            var newOwnerId = 2;
            var newOwner = Builder<Domain.Entities.User>.CreateNew()
                .With(x => x.Id, newOwnerId).Build();
            var pet = Builder<Domain.Entities.Pet>.CreateNew()
                .With(x => x.OldOwner, oldOwner)
                .With(x => x.OldOwnerId, oldOwnerId)
                .Build();

            var request = new AdoptRequest { PetId = pet.Id, NewOwnerId = oldOwnerId };

            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>())
                .ReturnsForAnyArgs(oldOwner);

            _petRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>())
                .ReturnsForAnyArgs(pet);

            var act = async () => await _handler.Handle(request, new CancellationToken());
            await act.Should().ThrowAsync<ValidationException>().WithMessage("You can not adopt your pet, you need to delete its adopt post.");
            await _petRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>());
            await _petRepository.DidNotReceive().Delete(Arg.Any<Domain.Entities.Pet>());
        }
    }
}






