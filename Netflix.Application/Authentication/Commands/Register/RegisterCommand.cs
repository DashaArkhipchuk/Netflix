using MediatR;
using Netflix.Application.Authentication.Common;

namespace Netflix.Application.Authentication.Commands.Register
{
    public record RegisterCommand
    (
        string FirstName,

        string LastName,

        string Email,

        DateOnly BirthDate,

        string Password,

        bool? IsActor
    ) : IRequest<AuthenticationResult>;
}
