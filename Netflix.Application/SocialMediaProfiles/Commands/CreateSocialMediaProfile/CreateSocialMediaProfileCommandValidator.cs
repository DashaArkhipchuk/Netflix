using FluentValidation;
using Netflix.Application.Common.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.SocialMediaProfiles.Commands.CreateSocialMediaProfile
{
    internal class CreateSocialMediaProfileCommandValidator: AbstractValidator<CreateSocialMediaProfileCommand>
    {
        public CreateSocialMediaProfileCommandValidator()
        {
            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Display name is required.")
                .MaximumLength(100).WithMessage("Display name must not exceed 100 characters.");

            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Nickname is required.")
                .MaximumLength(50).WithMessage("Nickname must not exceed 50 characters.")
                .Matches("^[a-zA-Z0-9_.]+$")
                .WithMessage("Nickname may only contain letters, numbers, dots and underscores.");

            RuleFor(x => x.AvatarUrl).MaximumLength(500)
                .Must(ValidationPredicateHelpers.BeAValidUrl).When(x => ValidationPredicateHelpers.BeNotNullOrWhitespace(x.AvatarUrl))
                .WithMessage("Avatar URL must be a valid URL.");

            RuleFor(x => x.BackgroundUrl).MaximumLength(500)
                .Must(ValidationPredicateHelpers.BeAValidUrl).When(x => ValidationPredicateHelpers.BeNotNullOrWhitespace(x.BackgroundUrl))
                .WithMessage("Background URL must be a valid URL.");

            RuleFor(x=>x.PhoneNumber).MaximumLength(100)
                .Must(ValidationPredicateHelpers.BeAValidPhoneNumber).When(x => ValidationPredicateHelpers.BeNotNullOrWhitespace(x.PhoneNumber))
                 .WithMessage("Phone number must be a valid phone number of any country.");

            RuleFor(x => x.Country).MaximumLength(100).WithMessage("Country information must not exceed 100 characters.");
            RuleFor(x => x.City).MaximumLength(100).WithMessage("City information must not exceed 100 characters.");
            RuleFor(x=>x.Gender).MaximumLength(20).WithMessage("Gender information must not exceed 50 characters.");
            RuleFor(x => x.AboutMeText).MaximumLength(1000).WithMessage("About Me section text must not exceed 1000 characters.");

        }

        
    }
}
