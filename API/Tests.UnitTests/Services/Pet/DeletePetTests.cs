using Application.Common.Exceptions;
using Application.Services.Pet.Delete;
using Domain.IRepositories;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Tests.UnitTests.Services.Pet
{
    public class DeletePetTests
    {
        private readonly DeletePetHandler _handler;
        private readonly IPetRepository _petRepository;
        private readonly IImageRepository _imageRepository;

        public DeletePetTests()
        {
            _petRepository = Substitute.For<IPetRepository>();
            _imageRepository = Substitute.For<IImageRepository>();
            _handler = new DeletePetHandler(_petRepository, _imageRepository);
        }

        [Fact]
        public async Task Should_Delete_Pet()
        {
            var ownerId = 1;
            var pet = Builder<Domain.Entities.Pet>.CreateNew()
                .With(x => x.OldOwnerId = ownerId)
                .With(x => x.Images = Builder<Domain.Entities.Image>.CreateListOfSize(3).Build())
                .Build();

            var request = new DeletePetRequest { Id = pet.Id, OwnerId = ownerId };

            _petRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>()).ReturnsForAnyArgs(pet);

            var response = await _handler.Handle(request, new CancellationToken());

            await _petRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>());
            await _petRepository.Received(1).Delete(Arg.Any<Domain.Entities.Pet>());
            await _imageRepository.Received(1).Delete(Arg.Any<IEnumerable<Domain.Entities.Image>>());
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Throw_NotFoundException_When_Pet_Not_Found()
        {
            var ownerId = 1;
            var petId = 2;
            var request = new DeletePetRequest { Id = petId, OwnerId = ownerId };

            _petRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>()).ReturnsNullForAnyArgs();

            var act = async () => await _handler.Handle(request, new CancellationToken());

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("Pet not found.");
            await _petRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>());
            await _petRepository.DidNotReceive().Delete(Arg.Any<Domain.Entities.Pet>());
            await _imageRepository.DidNotReceive().Delete(Arg.Any<IEnumerable<Domain.Entities.Image>>());
        }

        [Fact]
        public async Task Should_Throw_ValidationException_When_User_Not_Owner()
        {
            var ownerId = 1;
            var petOwnerId = 2;
            var pet = Builder<Domain.Entities.Pet>.CreateNew().With(x => x.OldOwnerId = petOwnerId).Build();
            var request = new DeletePetRequest { Id = pet.Id, OwnerId = ownerId };

            _petRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>()).ReturnsForAnyArgs(pet);

            var act = async () => await _handler.Handle(request, new CancellationToken());

            await act.Should().ThrowAsync<ValidationException>().WithMessage("You can not delete a post's pet that is not yours.");
            await _petRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.Pet, bool>>>());
            await _petRepository.DidNotReceive().Delete(Arg.Any<Domain.Entities.Pet>());
            await _imageRepository.DidNotReceive().Delete(Arg.Any<IEnumerable<Domain.Entities.Image>>());
        }
    }
}
