using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class Showing
{
    public Guid Id { get; set; }

    public Guid IdFilmSession { get; set; }

    public Guid IdDateTimeSession { get; set; }

    public virtual DateTimeSession IdDateTimeSessionNavigation { get; set; } = null!;

    public virtual FilmSession IdFilmSessionNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
