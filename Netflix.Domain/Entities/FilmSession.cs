using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class FilmSession
{
    public Guid Id { get; set; }

    public Guid IdFilm { get; set; }

    public Guid IdCinema { get; set; }

    public bool? Is3d { get; set; }

    public bool? IsSubtitles { get; set; }

    public virtual Cinema IdCinemaNavigation { get; set; } = null!;

    public virtual Film IdFilmNavigation { get; set; } = null!;

    public virtual ICollection<Showing> Showings { get; set; } = new List<Showing>();
}
