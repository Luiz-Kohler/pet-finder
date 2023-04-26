using FluentValidation;
using Application.Common.Extensions;

namespace Application.Services.User.Create
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 600).WithMessage("The name must be between 2 and 600 characters");
            RuleFor(x => x.Email).Must(x => x.IsValidEmail()).WithMessage("The email format must be correct");
            RuleFor(x => x.Password).NotEmpty().Length(4, 16).WithMessage("The password must be between 4 and 16 characters");
            RuleFor(x => x.State).NotEmpty().Length(2, 600).WithMessage("The state must be between 2 and 600 characters");
            RuleFor(x => x.City).NotEmpty().Length(2, 600).WithMessage("The city must be between 2 and 600 characters");

        }
    }
}
