using Application.Common.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Pet.Create
{
    public class CreatePetHandler : IRequestHandler<CreatePetRequest, CreatePetResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPetRepository _petRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public CreatePetHandler(
            IUserRepository userRepository,
            IPetRepository petRepository,
            IImageRepository imageRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _petRepository = petRepository;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<CreatePetResponse> Handle(CreatePetRequest request, CancellationToken cancellationToken)
        {
            var owner = await _userRepository.SelectOne(x => x.Id == request.OldOwnerId);

            if (owner is null)
                throw new NotFoundException("User not found with this id.");


            var pet = _mapper.Map<Domain.Entities.Pet>(request);
            await _petRepository.Create(pet);
            await _petRepository.SaveChanges();

            var images = request.Images?.Select(x => GetImage(x, pet.Id)).ToList();

            if (images is not null)
                await _imageRepository.Create(images);

            return new();
        }

        private Image GetImage(IFormFile image, int petId)
        {
            return new Domain.Entities.Image
            {
                Url = ":(",
                PetId = petId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
