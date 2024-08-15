namespace Netflix.Domain;

public partial class ActorModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
    public virtual ICollection<FilmActor> FilmActors { get; set; } = new List<FilmActor>();

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
    public virtual ICollection<SeriesActor> SeriesActors { get; set; } = new List<SeriesActor>();
}
