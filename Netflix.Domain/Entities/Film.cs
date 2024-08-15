namespace Netflix.Domain;

public partial class Film
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public decimal? Rating { get; set; }

    public DateOnly ReleaseDate { get; set; }

    public string? Country { get; set; }

    public TimeOnly? Duration { get; set; }

    public string? Director { get; set; }

    public string? ProductionCompanies { get; set; }

    public string? About { get; set; }

    public int AgeLimit { get; set; }

    public Guid? IdProduct { get; set; }

    public string PictureUrl { get; set; } = null!;

    public virtual ICollection<ActorModel> Actors { get; set; } = new List<ActorModel>();
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    public virtual ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();
    public virtual ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();

    public virtual ICollection<FilmSession> FilmSessions { get; set; } = new List<FilmSession>();

    public virtual Product? IdProductNavigation { get; set; }
}
