using Application.Common.Exceptions;
using Application.Common.Hash;
using Application.Common.JWT;
using Domain.IRepositories;
using MediatR;

namespace Application.Services.User.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IHashHandler _hashHandler;

        public LoginHandler(
            IUserRepository userRepository,
            IJwtHandler jwtHandler,
            IHashHandler hashHandler)
        {
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _hashHandler = hashHandler;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var passwrordHashed = _hashHandler.Hash(request.Password);

            var user = await _userRepository.SelectOne(x => x.Email == request.Email && x.Password == passwrordHashed);

            if (user is null)
                throw new NotFoundException("The credentials is wrong.");

            var token = _jwtHandler.CreateToken(user);

            var response = new LoginResponse()
            {
                Token = token
            };

            return response;
        }
    }
}
