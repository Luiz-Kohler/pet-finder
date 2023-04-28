using Application.Common.Exceptions;
using Application.Common.Hash;
using Application.Common.JWT;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace Application.Services.User.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        private readonly IHashHandler _hashHandler;

        public CreateUserHandler(
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IMapper mapper,
            IJwtHandler jwtHandler,
            IHashHandler hashHandler)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
            _jwtHandler = jwtHandler;
            _hashHandler = hashHandler;
        }

        public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userWithSameEmail = await _userRepository.SelectOne(x => x.Email == request.Email);

            if (userWithSameEmail is not null)
                throw new DuplicateValueException("User already exists with the same email informed.");

            var address = await CreateAddress(request);
            var user = await CreateUser(request, address);

            var token = _jwtHandler.CreateToken(user);

            return new()
            {
                Token = token
            }; ;
        }

        private async Task<Address> CreateAddress(CreateUserRequest request)
        {
            var address = _mapper.Map<Address>(request);

            await _addressRepository.Create(address);
            await _addressRepository.SaveChanges();

            return address;
        }

        private async Task<Domain.Entities.User> CreateUser(CreateUserRequest request, Address address)
        {
            var user = _mapper.Map<Domain.Entities.User>(request);

            user.Password = _hashHandler.Hash(user.Password);

            user.AddressId = address.Id;
            user.Address = address;

            await _userRepository.Create(user);
            await _userRepository.SaveChanges();

            return user;
        }
    }
}
