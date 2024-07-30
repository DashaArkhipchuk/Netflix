using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class Series
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

    public int SeasonCount { get; set; }

    public int EpisodeCount { get; set; }

    public bool? IsFinished { get; set; }

    public Guid? IdProduct { get; set; }

    public string PictureUrl { get; set; } = null!;

    public virtual Product? IdProductNavigation { get; set; }

    public virtual ICollection<ActorModel> Actors { get; set; } = new List<ActorModel>();
    public virtual ICollection<SeriesActor> SeriesActors { get; set; } = new List<SeriesActor>();

    public virtual ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();
    public virtual ICollection<SeriesGenre> SeriesGenres { get; set; } = new List<SeriesGenre>();
}
