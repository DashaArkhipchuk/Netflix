using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class SeriesGenre
{
    public Guid Id { get; set; }

    public Guid IdSeries { get; set; }

    public Guid IdGenreInSeries { get; set; }

    public virtual GenreModel IdGenreInSeriesNavigation { get; set; } = null!;

    public virtual Series IdSeriesNavigation { get; set; } = null!;
}
