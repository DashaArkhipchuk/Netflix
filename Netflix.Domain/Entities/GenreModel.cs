using System;
using System.Collections.Generic;

namespace Netflix.Domain;

public partial class GenreModel
{
    public Guid Id { get; set; }

    public string? GenreName { get; set; }
    
    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
    public virtual ICollection<FilmGenre> FilmGenres { get; set; } = new List<FilmGenre>();

    public virtual ICollection<Series> Series { get; set; } = new List<Series>();
    public virtual ICollection<SeriesGenre> SeriesGenres { get; set; } = new List<SeriesGenre>();

    public override string? ToString()
    {
        return GenreName;
    }
}
