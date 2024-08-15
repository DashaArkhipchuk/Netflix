using Netflix.Domain;
using Bogus;

namespace Netflix.Infrastructure.Seeders
{
    internal class FilmSeeder(NetflixProjectContext dbContext)
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Films.Any())
                {
                    var films = GetFilms();
                }
            }
        }

        private IEnumerable<Film> GetFilms()
        {
            var genre = new Faker<GenreModel>()
                .RuleFor(g => g.GenreName, f => f.PickRandom<Genres>().ToString());
            var actor = new Faker<ActorModel>()
                .RuleFor(g => g.Name, f => f.Name.FullName());
            var film = new Faker<Film>()
                .RuleFor(o => o.Name, f => (f.Random.Bool() ? f.Hacker.Verb() : f.Hacker.Adjective() + " " + f.Hacker.Noun()))
                .RuleFor(o => o.Genres, f => genre.Generate(f.Random.Number(1, 3)))
                .RuleFor(o => o.About, f => f.Lorem.Text())
                .RuleFor(o => o.AgeLimit, f => f.Random.Number(0, 18))
                .RuleFor(o => o.Country, f => f.Address.Country())
                .RuleFor(o => o.Duration, f => new TimeOnly(f.Random.Number(0, 3), f.Random.Number(1, 59)))
                .RuleFor(o => o.Director, f => f.Name.FullName())
                .RuleFor(o => o.ProductionCompanies, f => f.Company.CompanyName())
                .RuleFor(o => o.Rating, f => f.Random.Decimal(0.0m, 10.0m))
                .RuleFor(o => o.ReleaseDate, f => f.Date.BetweenDateOnly(new DateOnly(1970, 1, 1), new DateOnly(2024, 1, 1)))
                .RuleFor(o => o.Actors, f => actor.Generate(f.Random.Number(3, 10)));
            return film.Generate(15);
        }

        public enum Genres
        {
            Drama,
            Adventure,
            Horror,
            Animation,
            Anime,
            Dorama,
            Thriller,
            Action,
            Comedy,
            Crime,
            Documentary,
            Fantasy,
            Romance,
            SciFi
        }
    }
}
