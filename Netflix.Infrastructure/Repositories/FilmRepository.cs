using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.IRepository;

namespace Netflix.Infrastructure.Repositories
{
    internal class FilmRepository(NetflixProjectContext dbContext) : IFilmRepository
    {
        public async Task<IReadOnlyList<Film>> GetAllAsync(int skip, int take)
        {
            var films = await dbContext.Films.Skip(skip).Take(take).ToListAsync();
            return films ?? new List<Film>();
        }

        
    }
}
