namespace Netflix.Domain;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid IdShowing { get; set; }

    public Guid IdClient { get; set; }

    public int? TicketCount { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Showing IdShowingNavigation { get; set; } = null!;
}
