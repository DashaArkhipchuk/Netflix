namespace Netflix.Domain;

public partial class DateTimeSession
{
    public Guid Id { get; set; }

    public TimeOnly TimeOfSession { get; set; }

    public DateOnly DateOfSession { get; set; }

    public virtual ICollection<Showing> Showings { get; set; } = new List<Showing>();
}
