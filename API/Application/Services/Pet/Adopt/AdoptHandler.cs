using Application.Common.Exceptions;
using AutoMapper;
using Domain.Documents;
using Domain.IRepositories;
using MediatR;

namespace Application.Services.Pet.Adopt
{
    public class AdoptHandler : IRequestHandler<AdoptRequest, AdoptResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPetRepository _petRepository;
        private readonly IAdoptionRecordRepository _adoptionRecordRepository;
        private readonly IMapper _mappper;

        public AdoptHandler(
            IUserRepository userRepository,
            IPetRepository petRepository,
            IAdoptionRecordRepository adoptionRecordRepository,
            IMapper mappper)
        {
            _userRepository = userRepository;
            _petRepository = petRepository;
            _adoptionRecordRepository = adoptionRecordRepository;
            _mappper = mappper;
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
            pet.NewOwner = newOnwer;

            await _petRepository.Update(pet);
            await CreateAdoptionRecord(pet);

            return new();
        }

        private async Task CreateAdoptionRecord(Domain.Entities.Pet pet)
        {
            var oldOwnerRecord = _mappper.Map<Owner>(pet.OldOwner);
            var newOwnerRecord = _mappper.Map<Owner>(pet.NewOwner);
            var petRecord = _mappper.Map<AdoptedPet>(pet);

            var adoptionRecord = new AdoptionRecord
            {
                CreatedAt = DateTime.UtcNow,
                NewOwner = newOwnerRecord,
                OldOwner = oldOwnerRecord,
                Pet = petRecord
            };

            await _adoptionRecordRepository.Create(adoptionRecord);
        }
    }
}
