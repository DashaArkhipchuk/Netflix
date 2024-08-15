namespace Netflix.Domain;

public partial class FilmGenre
{
    public Guid Id { get; set; }

    public Guid IdFilm { get; set; }

    public Guid IdGenreInFilm { get; set; }

    public virtual Film IdFilmNavigation { get; set; } = null!;

    public virtual GenreModel IdGenreInFilmNavigation { get; set; } = null!;
}
