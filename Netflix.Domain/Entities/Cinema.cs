namespace Netflix.Domain;

public partial class Cinema
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<FilmSession> FilmSessions { get; set; } = new List<FilmSession>();
}
