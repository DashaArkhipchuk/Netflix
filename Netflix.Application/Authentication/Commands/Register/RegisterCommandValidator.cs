using FluentValidation;

namespace Netflix.Application.Authentication.Commands.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name cannot be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name cannot be empty");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("The value must be valid email address");
            RuleFor(x => x.BirthDate).NotEmpty().InclusiveBetween(new DateOnly(1920,1,1), DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("Birth date must be between 1/1/1920 and today");
            RuleFor(x => x.Password).NotEmpty().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&-+=()!? \"]).{8,20}$").WithMessage("A password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit and one of the special characters and must be between 8 to 20 characters in length");
        }
    }
}
