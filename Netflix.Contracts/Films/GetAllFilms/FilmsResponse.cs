namespace Netflix.Contracts.Films.GetAllFilms
{
    public record FilmsResponse
    (
        IReadOnlyList<FilmDto> films
    );
}
