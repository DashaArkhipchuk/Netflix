using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirector.Commands.CreateCastingDirectorProfile
{
    public class CreateCastingDirectorProfileCommandValidator : AbstractValidator<CreateCastingDirectorProfileCommand>
    {
        public CreateCastingDirectorProfileCommandValidator()
        {
            RuleFor(x => x.ClientId)
                .NotEmpty().WithMessage("ClientId is required.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("FullName is required.")
                .MaximumLength(255).WithMessage("FullName must be 255 characters or less.");

            RuleFor(x => x.TypeId)
                .NotEmpty().WithMessage("TypeId is required.");

            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("CompanyName is required.")
                .MaximumLength(255).WithMessage("CompanyName must be 255 characters or less.");

            RuleFor(x => x.Website)
               .MaximumLength(255).WithMessage("CompanyName must be 255 characters or less.")
               .Matches(@"^https:\/\/([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,6}(:[0-9]{1,5})?(\/.*)?$")
               .WithMessage("Website must be a valid URL format.")
               .When(x => !string.IsNullOrEmpty(x.Website));

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(255).WithMessage("Address must be 255 characters or less.");


            RuleFor(x => x.RegionName)
                .NotEmpty().WithMessage("RegionName is required.")
                .MaximumLength(255).WithMessage("RegionName must be 255 characters or less.");


            RuleFor(x => x.PhoneNumberWithCountryCode)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+\d{1,3}\s?\d{1,14}(\s?\d{1,13})?$")
                .WithMessage("PhoneNumber must include country code and be in a valid format.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");

        }
    }
}
