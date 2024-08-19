using Microsoft.EntityFrameworkCore;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Repositories
{
    internal class SeriesRepository(NetflixProjectContext dbContext) : ISeriesRepository
    {

        public async Task<List<Series>> GetAllAsync(int skip, int take, List<string> genres, bool sortByLatest = false, decimal? minimumRating = null, int? year = null, int? episodes = null)
        {
            // Start with the base query
            var query = dbContext.Series.Include(s=>s.Genres).AsQueryable();

            // Apply genre filter if provided
            if (genres != null && genres.Count != 0)
            {
                var lowerGenres = genres.Select(g => g.ToLower()).ToList();
                query = query.Where(series => lowerGenres.All(genre =>
                    series.Genres.Select(g => (g.GenreName ?? "").ToLower()).Contains(genre)));
            }

            // Apply minimum rating filter if provided
            if (minimumRating.HasValue)
            {
                query = query.Where(series => series.Rating >= minimumRating.Value);
            }

            // Apply year filter if provided
            if (year.HasValue)
            {
                query = query.Where(series => series.ReleaseDate.Year == year.Value);
            }

            // Apply episodes filter if provided
            if (episodes.HasValue)
            {
                query = query.Where(series => series.EpisodeCount == episodes.Value);
            }

            // Apply sorting by date if provided
            if (sortByLatest)
            {
                query = query.OrderByDescending(series => series.ReleaseDate);
            }

            // Apply pagination
            query = query.Skip(skip).Take(take);

            // Execute the query and return results
            return await query.ToListAsync() ?? new List<Series>();
        }

        public async Task<Series?> GetByIdAsync(Guid? id)
        {
            return await dbContext.Series.Include(s=>s.Actors).Include(s=>s.Genres).SingleOrDefaultAsync(series => series.Id == id);
        }
    }
}
