using FluentValidation;
using Application.Common.Extensions;

namespace Application.Services.User.Login
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).Must(x => x.IsValidEmail()).WithMessage("The email format must be correct");
            RuleFor(x => x.Password).NotEmpty().Length(4, 16).WithMessage("The password must be between 4 and 16 characters");
        }
    }
}
