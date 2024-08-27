using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Series.GetSeriesById
{
    public class SeriesEpisodesDto
    {
        public Guid EpisodeId { get; set; }
        public Guid SeriesId { get; set; }
        public string EpisodeName { get; set; } = null!;
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public int EpisodeNumberInSeason { get; set; }
        public string? PictureURL { get; set; }
        public string? VideoURL { get; set; }
    }
}
