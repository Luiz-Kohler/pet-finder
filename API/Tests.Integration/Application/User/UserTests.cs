using Application.Services.User.Create;
using Application.Services.User.Login;
using FizzWare.NBuilder;
using FluentAssertions;
using MongoDB.Driver;

namespace Tests.Integration.Application.User
{
    public class UserTests : ApplicationTestBase
    {
        [Fact]
        public async Task Should_Create_User()
        {
            var request = Builder<CreateUserRequest>
                .CreateNew()
                .With(x => x.Email, "emailexemplo@gmail.com")
                .Build();

            await Handle<CreateUserRequest, CreateUserResponse>(request);

            var users = GetEntities<Domain.Entities.User>();
            users.Count().Should().Be(1);
        }

        [Fact]
        public async Task Should_Login()
        {
            var email = "emailexemplo@gmail.com";
            var password = "password";

            var createUserRequest = Builder<CreateUserRequest>
                           .CreateNew()
                           .With(x => x.Email, email)
                           .With(x => x.Password, password)
                           .Build();

            await Handle<CreateUserRequest, CreateUserResponse>(createUserRequest);

            var request = Builder<LoginRequest>
                .CreateNew()
                .With(x => x.Email, email)
                .With(x => x.Password, password)
                .Build();

            var response = await Handle<LoginRequest, LoginResponse>(request);

            response.Should().NotBeNull();
        }
    }
}
