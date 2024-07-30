using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class FilmActor
{
    public Guid Id { get; set; }

    public Guid IdFilm { get; set; }

    public Guid IdActorsInRoles { get; set; }

    public virtual ActorModel IdActorsInRolesNavigation { get; set; } = null!;

    public virtual Film IdFilmNavigation { get; set; } = null!;
}
