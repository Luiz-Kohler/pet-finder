using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Pet.Create
{
    public class CreatePetValidator : AbstractValidator<CreatePetRequest>
    {
        public CreatePetValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 600).WithMessage("The name must be between 2 and 600 characters");
            RuleFor(x => x.Age).InclusiveBetween(0, 100).WithMessage("The age must be valid");
            RuleFor(x => x.Description).NotEmpty().Length(2, 600).WithMessage("The description must be between 2 and 600 characters");
            RuleFor(x => x.Type).IsInEnum().WithMessage("The type must be informed");
            RuleFor(x => x.Size).IsInEnum().WithMessage("The size must be informed");
            RuleFor(x => x.Images).Must(ValidateExtensionFiles).WithMessage("type with less than 500 Kilobytes");
            RuleFor(x => x.Images).Must(ValidateSizeFiles).WithMessage("The images must be (.jpg or .png)");
            RuleFor(x => x).Must(ValidateQuantityFiles).WithMessage("You can only send three images maximum");
        }

        private bool ValidateExtensionFiles(List<IFormFile> files)
        {
            if (files == null)
                return true;

            foreach (var file in files)
            {
                if (!ValidateExtensionFiles(file))
                    return false;
            }

            return true;
        }

        private bool ValidateSizeFiles(List<IFormFile> files)
        {
            if (files == null)
                return true;

            foreach (var file in files)
            {
                if (!ValidateSizeFiles(file))
                    return false;
            }

            return true;
        }


        private bool ValidateExtensionFiles(IFormFile file)
        {
            var validExtensions = new List<string>() { ".jpg", ".png" };
            var extension = Path.GetExtension(file.FileName);

            if (!validExtensions.Contains(extension.ToLower()))
                return false;

            return true;
        }

        private bool ValidateSizeFiles(IFormFile file)
        {
            if (file is null || file.Length > 500000)
                return false;

            return true;
        }

        private bool ValidateQuantityFiles(CreatePetRequest request)
        {
            if (request.Images is null)
                return true;

            if (request.Images.Count() > 3)
                return false;

            return true;
        }
    }
}
