using Application.Common.Exceptions;
using Domain.IRepositories;
using MediatR;

namespace Application.Services.Pet.Adopt
{
    public class AdoptHandler : IRequestHandler<AdoptRequest, AdoptResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPetRepository _petRepository;

        public AdoptHandler(
            IUserRepository userRepository,
            IPetRepository petRepository)
        {
            _userRepository = userRepository;
            _petRepository = petRepository;
        }

        public async Task<AdoptResponse> Handle(AdoptRequest request, CancellationToken cancellationToken)
        {

            var newOnwer = await _userRepository.SelectOne(x => x.Id == request.NewOwnerId && x.IsActive);

            if (newOnwer is null)
                throw new NotFoundException("User not found.");

            var pet = await _petRepository.SelectOne(x => x.Id == request.PetId && x.IsActive);

            if (pet is null)
                throw new NotFoundException("Pet not found.");

            if (pet.OldOwnerId == newOnwer.Id)
                throw new ValidationException("You can not adopt your pet, you need to delete its adopt post.");

            pet.AdoptDate = DateTime.UtcNow;
            pet.NewOwnerId = newOnwer.Id;

            await _petRepository.Update(pet);

            return new();
        }
    }
}
