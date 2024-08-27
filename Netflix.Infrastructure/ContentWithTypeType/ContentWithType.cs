using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ContentWithTypeType
{
    public class ContentWithType
    {
        public string Type { get; set; } = null!;
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string PictureUrl { get; set; } = null!;

        public decimal? Rating { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int? EpisodeCount { get; set; }

        public virtual ICollection<GenreModel> Genres { get; set; } = new List<GenreModel>();
        
        public virtual Film? Film { get; set; }
        public virtual Series? Series { get; set; }
    }
}
