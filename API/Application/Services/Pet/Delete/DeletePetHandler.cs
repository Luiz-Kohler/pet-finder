using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;

namespace Application.Services.Pet.Delete
{
    public class DeletePetHandler : IRequestHandler<DeletePetRequest, DeletePetResponse>
    {
        private readonly IPetRepository _petRepository;
        private readonly IImageRepository _imageRepository;

        public DeletePetHandler(
            IPetRepository petRepository,
            IImageRepository imageRepository)
        {
            _petRepository = petRepository;
            _imageRepository = imageRepository;
        }

        public async Task<DeletePetResponse> Handle(DeletePetRequest request, CancellationToken cancellationToken)
        {
            var pet = await _petRepository.SelectOne(x => x.Id == request.Id && x.NewOwnerId == null && x.IsActive);

            if(pet is null)
                throw new NotFoundException("Pet not found.");

            if (pet.OldOwnerId != request.OwnerId)
                throw new ValidationException("You can not delete a post's pet that is not yours.");

            await _petRepository.Delete(pet);

            if (pet.Images.Any())
                await _imageRepository.Delete(pet.Images);

            return new();
        }
    }
}
