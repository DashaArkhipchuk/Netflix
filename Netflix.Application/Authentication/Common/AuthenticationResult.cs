using Netflix.Domain;

namespace Netflix.Application.Authentication.Common
{
    public record AuthenticationResult
    (
        Client Client,
        string Token
    );

}
