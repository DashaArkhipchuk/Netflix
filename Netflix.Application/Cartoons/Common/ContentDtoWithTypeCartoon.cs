using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Cartoons.Common
{
    public class ContentDtoWithTypeCartoon
    {
        public string Type { get; set; } = null!;
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string PictureUrl { get; set; } = null!;

        public decimal? Rating { get; set; }

        public int ReleaseYear { get; set; }
    }
}
