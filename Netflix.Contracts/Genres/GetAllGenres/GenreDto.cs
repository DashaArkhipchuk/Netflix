using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Contracts.Genres.GetAllGenres
{
    public class GenreDto
    {
        public Guid Id { get; set; }

        public string? GenreName { get; set; }
    }
}
