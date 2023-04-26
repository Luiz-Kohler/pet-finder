using Application.Services.Pet.Delete;
using FluentValidation;

namespace Application.Services.Pet.Adopt
{
    public class AdoptValidator : AbstractValidator<AdoptRequest>
    {
        public AdoptValidator()
        {
            RuleFor(x => x.NewOwnerId).GreaterThanOrEqualTo(0).WithMessage("User not found");
            RuleFor(x => x.PetId).GreaterThanOrEqualTo(0).WithMessage("Pet not found");

        }
    }
}
