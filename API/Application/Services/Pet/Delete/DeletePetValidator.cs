using FluentValidation;

namespace Application.Services.Pet.Delete
{
    public class DeletePetValidator : AbstractValidator<DeletePetRequest>
    {
        public DeletePetValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).WithMessage("Pet not found");
        }
    }
}
