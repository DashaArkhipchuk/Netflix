namespace Netflix.Contracts.Authentication.Register
{
    public record RegisterRequest
    (
        string FirstName,

        string LastName,

        string Email,

        DateOnly BirthDate,

        string Password,

        bool? IsActor,
        bool? IsCastingDirector
    );
}
