﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Series.GetSeriesById
{
    public class SeriesExtendedDto
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

       

        public string PictureUrl { get; set; } = null!;

        
        public virtual ICollection<string> Actors { get; set; } = new List<string>();
        

        public virtual ICollection<string> Genres { get; set; } = new List<string>();

        public virtual ICollection<SeriesEpisodesDto> SeriesEpisodes { get; set; } = new List<SeriesEpisodesDto>();

    }
}
