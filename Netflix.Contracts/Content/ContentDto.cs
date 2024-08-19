using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Common
{
    public class ContentDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string PictureUrl { get; set; } = null!;

        public decimal? Rating { get; set; }

        public int ReleaseYear { get; set; }
    }
}
