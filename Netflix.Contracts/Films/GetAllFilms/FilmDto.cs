namespace Netflix.Contracts.Films.GetAllFilms
{
    public class FilmDto
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public decimal? Rating { get; set; }

        public int ReleaseYear { get; set; }
    }
}
