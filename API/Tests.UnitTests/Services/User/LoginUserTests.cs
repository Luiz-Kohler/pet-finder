using Application.Common.Exceptions;
using Application.Common.Hash;
using Application.Common.JWT;
using Application.Services.User.Login;
using Bogus;
using Domain.IRepositories;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace Tests.UnitTests.Services.User
{
    public class LoginUserTests
    {
        private readonly LoginHandler _handler;
        private readonly Faker _faker;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IHashHandler _hashHandler;

        public LoginUserTests()
        {
            _faker = new();
            _userRepository = Substitute.For<IUserRepository>();
            _jwtHandler = Substitute.For<IJwtHandler>();
            _hashHandler = Substitute.For<IHashHandler>();
            _handler = new LoginHandler(
                _userRepository,
                _jwtHandler,
                _hashHandler);
        }

        [Fact]
        public async Task Should_Login()
        {
            var user = Builder<Domain.Entities.User>.CreateNew().Build();

            var request = Builder<LoginRequest>
                .CreateNew()
                .With(x => x.Email, "emailexemplo@gmail.com")
                .Build();

            var expectedToken = _faker.Random.Word();

            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>()).ReturnsForAnyArgs(user);
            _hashHandler.Hash(Arg.Any<string>()).ReturnsForAnyArgs(_faker.Random.Word());
            _jwtHandler.CreateToken(Arg.Any<Domain.Entities.User>()).ReturnsForAnyArgs(expectedToken);

            var response = await _handler.Handle(request, new CancellationToken());

            await _userRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());
            _jwtHandler.Received(1).CreateToken(Arg.Any<Domain.Entities.User>());
            _hashHandler.Received(1).Hash(Arg.Any<string>());
            response.Token.Should().Be(expectedToken);
        }
        [Fact]
        public async Task Should_Return_Error_When_Credentials_Are_Wrong()
        {
            var request = Builder<LoginRequest>
                .CreateNew()
                .With(x => x.Email, "emailexemplo@gmail.com")
                .Build();

            _hashHandler.Hash(Arg.Any<string>()).ReturnsForAnyArgs(_faker.Random.Word());
            _userRepository.SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>()).ReturnsNull();

            var act = async () => await _handler.Handle(request, new CancellationToken());

            await act.Should().ThrowAsync<NotFoundException>().WithMessage("The credentials are wrong.");
            await _userRepository.Received().SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());
            _hashHandler.Received(1).Hash(Arg.Any<string>());
            _jwtHandler.DidNotReceive().CreateToken(Arg.Any<Domain.Entities.User>());
        }
    }
}