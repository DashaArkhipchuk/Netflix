namespace Netflix.Contracts.Films.GetAllFilms
{
    public record GetAllFilmsRequest
    (
        int skip,
        int take
    );
}
