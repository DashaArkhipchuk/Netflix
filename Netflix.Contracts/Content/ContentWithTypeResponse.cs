using Netflix.Contracts.Films.GetFilmById;
using Netflix.Contracts.Series.GetSeriesById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Content
{
    public class ContentWithTypeResponse
    {
        public string Type { get; set; } = null!;

        public virtual FilmExtendedDto? Film { get; set; }
        public virtual SeriesExtendedDto? Series { get; set; }
    }
}
