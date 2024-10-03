namespace Netflix.Contracts.Authentication.Common
{
    public record AuthenticationResponse
    (
        Guid Id,

        string FirstName,

        string LastName,

        string Email,

        DateOnly BirthDate,

        bool? IsActor,

        bool? IsCastingDirector,

        string Token
    );
}
