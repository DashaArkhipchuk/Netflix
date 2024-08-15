using FluentValidation;

namespace Netflix.Application.Authentication.Queries.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator() 
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("The value must be a valid email address");
            RuleFor(x => x.Password).NotEmpty().Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&-+=()!? \"]).{8,20}$").WithMessage("A password must contain at least 1 lowercase letter, 1 uppercase letter, 1 digit and one of the special characters and must be between 8 to 20 characters in length");
        }
    }
}
