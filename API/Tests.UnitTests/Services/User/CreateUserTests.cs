using Application.Common.Hash;
using Application.Common.JWT;
using Application.Services.User.Create;
using AutoMapper;
using Bogus;
using Domain.Entities;
using Domain.IRepositories;
using FizzWare.NBuilder;
using FluentAssertions;
using Moq;
using NSubstitute;
using System.Linq.Expressions;

namespace Tests.UnitTests.Services.User
{
    public class CreateUserTests
    {
        private readonly CreateUserHandler _handler;
        private readonly Faker _faker;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        private readonly IHashHandler _hashHandler;

        public CreateUserTests()
        {
            _faker = new();
            _userRepository = Substitute.For<IUserRepository>();
            _addressRepository = Substitute.For<IAddressRepository>();
            _mapper = Substitute.For<IMapper>();
            _jwtHandler = Substitute.For<IJwtHandler>();
            _hashHandler = Substitute.For<IHashHandler>();
            _handler = new CreateUserHandler(
                _userRepository,
                _addressRepository,
                _mapper,
                _jwtHandler,
                _hashHandler);
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var user = Builder<Domain.Entities.User>.CreateNew().Build();
            var address = Builder<Address>.CreateNew().Build();

            var request = Builder<CreateUserRequest>
                .CreateNew()
                .With(x => x.Email, "emailexemplo@gmail.com")
                .Build();

            var expectedToken = _faker.Random.Word();

            _mapper.Map<Address>(Arg.Any<CreateUserRequest>()).ReturnsForAnyArgs(address);
            _mapper.Map<Domain.Entities.User>(Arg.Any<CreateUserRequest>()).ReturnsForAnyArgs(user);
            _jwtHandler.CreateToken(Arg.Any<Domain.Entities.User>()).ReturnsForAnyArgs(expectedToken);
            _hashHandler.Hash(Arg.Any<string>()).ReturnsForAnyArgs(_faker.Random.Word());

            var response = await _handler.Handle(request, new CancellationToken());

            await _userRepository.Received(1).SelectOne(Arg.Any<Expression<Func<Domain.Entities.User, bool>>>());
            _mapper.Received(1).Map<Address>(Arg.Any<CreateUserRequest>());
            await _addressRepository.Received(1).Create(Arg.Any<Address>());
            _mapper.Received(1).Map<Domain.Entities.User>(Arg.Any<CreateUserRequest>());
            await _userRepository.Received(1).Create(Arg.Any<Domain.Entities.User>());
            _jwtHandler.Received(1).CreateToken(Arg.Any<Domain.Entities.User>());
            _hashHandler.Received(1).Hash(Arg.Any<string>());
            response.Token.Should().Be(expectedToken);
        }
    }
}
