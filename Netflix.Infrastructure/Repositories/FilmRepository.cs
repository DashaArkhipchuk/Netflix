using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.IRepository;

namespace Netflix.Infrastructure.Repositories
{
    internal class FilmRepository(NetflixProjectContext dbContext) : IFilmRepository
    {
        public async Task<List<Film>> GetAllAsync(int skip, int take, List<string> genres, bool sortByLatest = false, decimal? minimumRating = null, int? year = null, int? episodes = null)
        {
            // Start with the base query
            var query = dbContext.Films.Include(f => f.Genres).AsQueryable();

            // Apply genre filter if provided
            if (genres!=null && genres.Count != 0)
            {
                var lowerGenres = genres.Select(g => g.ToLower()).ToList();
                query = query.Where(film => lowerGenres.All(genre =>
                    film.Genres.Select(g => (g.GenreName ?? "").ToLower()).Contains(genre)));
            }

            // Apply minimum rating filter if provided
            if (minimumRating.HasValue)
            {
                query = query.Where(film => film.Rating >= minimumRating.Value);
            }


            // Apply year filter if provided
            if (year.HasValue)
            {
                query = query.Where(film => film.ReleaseDate.Year == year.Value);
            }

            // Apply sorting by date if provided
            if (sortByLatest)
            {
                query = query.OrderByDescending(film => film.ReleaseDate);
            }

            // Apply pagination
            query = query.Skip(skip).Take(take);


            // Execute the query and return results
            return await query.ToListAsync() ?? new List<Film>();
        }

        public async Task<Film?> GetByIdAsync(Guid? id)
        {
            return await dbContext.Films.Include(f=>f.Actors).Include(f=>f.Genres).SingleOrDefaultAsync(film => film.Id == id);
        }
    }
}
