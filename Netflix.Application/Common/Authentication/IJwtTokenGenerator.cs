using Netflix.Domain;

namespace Netflix.Application.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Client client);
    }
}
