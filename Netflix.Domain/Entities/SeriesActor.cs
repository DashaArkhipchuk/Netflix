using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class SeriesActor
{
    public Guid Id { get; set; }

    public Guid IdSeries { get; set; }

    public Guid IdActorsInRoles { get; set; }

    public virtual ActorModel IdActorsInRolesNavigation { get; set; } = null!;

    public virtual Series IdSeriesNavigation { get; set; } = null!;
}
