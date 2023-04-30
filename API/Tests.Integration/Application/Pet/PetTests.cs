using Application.Services.Pet.Adopt;
using Application.Services.Pet.Create;
using Application.Services.Pet.Delete;
using Application.Services.Pet.GetMany;
using Application.Services.User.Create;
using FizzWare.NBuilder;
using FluentAssertions;
using System.Runtime.InteropServices;

namespace Tests.Integration.Application.Pet
{
    public class PetTests : ApplicationTestBase
    {
        [Fact]
        public async Task Should_Create_Pet()
        {
            await CreateUser();
            var user = GetEntities<Domain.Entities.User>().First();

            var request = Builder<CreatePetRequest>
                .CreateNew()
                .With(x => x.OldOwnerId, user.Id)
                .Build();

            await Handle<CreatePetRequest, CreatePetResponse>(request);

            var pets = GetEntities<Domain.Entities.Pet>();
            pets.Count.Should().Be(1);
        }

        [Fact]
        public async Task Should_Get_Many_Pets()
        {
            var user = await CreateUser();
            await CreatePet(user);

            var request = new GetManyPetsRequest();

            var response = await Handle<GetManyPetsRequest, GetManyPetsResponse>(request);

            var pets = GetEntities<Domain.Entities.Pet>();
            pets.Count.Should().Be(response.Pets.Count);
        }

        [Fact]
        public async Task Should_Delete_Pet()
        {
            var user = await CreateUser();
            var pet = await CreatePet(user);

            var request = Builder<DeletePetRequest>
                .CreateNew()
                .With(x => x.Id, pet.Id)
                .With(x => x.OwnerId, user.Id)
                .Build(); ;

            await Handle<DeletePetRequest, DeletePetResponse>(request);

            var pets = GetEntities<Domain.Entities.Pet>();
            pets.All(x => !x.IsActive).Should().Be(true);
        }

        private async Task<Domain.Entities.User> CreateUser(string email = "emailexemplo@gmail.com")
        {
            var request = Builder<CreateUserRequest>
               .CreateNew()
               .With(x => x.Email, email)
               .Build();

            await Handle<CreateUserRequest, CreateUserResponse>(request);

            return GetEntities<Domain.Entities.User>().First();
        }

        private async Task<Domain.Entities.Pet> CreatePet(Domain.Entities.User user)
        {
            var request = Builder<CreatePetRequest>
               .CreateNew()
               .With(x => x.OldOwnerId, user.Id)
               .Build();

            await Handle<CreatePetRequest, CreatePetResponse>(request);

            return GetEntities<Domain.Entities.Pet>().First();

        }
    }
}
