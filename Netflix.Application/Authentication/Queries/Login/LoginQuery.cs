using MediatR;
using Netflix.Application.Authentication.Common;

namespace Netflix.Application.Authentication.Queries
{
    public record LoginQuery
    (
        string Email,

       string Password
    ): IRequest<AuthenticationResult>;
}
