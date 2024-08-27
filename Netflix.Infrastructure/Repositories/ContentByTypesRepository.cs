using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.ContentWithTypeType;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Netflix.Infrastructure.Seeders.FilmSeeder;

namespace Netflix.Infrastructure.Repositories
{
    public class ContentByTypesRepository : IContentByTypesRepository
    {
        private readonly NetflixProjectContext _dbContext;

        public string? Type { get; set; }

        public ContentByTypesRepository(NetflixProjectContext dbContext) => _dbContext = dbContext;
        public async Task<List<ContentWithType>> GetAllAsync(int skip, int take, List<string> genres, bool sortByLatest = false, decimal? minimumRating = null, int? year = null, int? episodes = null)
        {
            var queryFilm = await _dbContext.Films.Where(x=>x.Genres.Select(g=>g.GenreName.ToLower()).Contains(Type.ToLower())).Select(x => new ContentWithType { Id = x.Id, Name = x.Name, Genres = x.Genres, PictureUrl = x.PictureUrl, Rating = x.Rating, ReleaseDate = x.ReleaseDate, Type = "film", Film = x, EpisodeCount = null }).ToListAsync();
            var querySeries = await _dbContext.Series.Where(x=>x.Genres.Select(g=>g.GenreName.ToLower()).Contains(Type.ToLower())).Select(x => new ContentWithType { Id = x.Id, Name = x.Name, Genres = x.Genres, PictureUrl = x.PictureUrl, Rating = x.Rating, ReleaseDate = x.ReleaseDate, Type = "series", Series = x, EpisodeCount = x.EpisodeCount }).ToListAsync();

            var query = queryFilm.Concat(querySeries).AsQueryable();


            // Apply genre filter if provided
            if (genres != null && genres.Count != 0)
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

            // Apply episodes filter if provided
            if (episodes.HasValue)
            {
                query = query.Where(series => series.EpisodeCount == episodes.Value);
            }

            // Apply sorting by date if provided
            if (sortByLatest)
            {
                query = query.OrderByDescending(film => film.ReleaseDate);
            }

            // Apply pagination
            query = query.Skip(skip).Take(take);


            // Execute the query and return results
            return query.ToList() ?? new List<ContentWithType>();
        }

        public async Task<ContentWithType?> GetByIdAsync(Guid? id)
        {
            var queryFilm = await _dbContext.Films.Include(x => x.Genres).Include(x => x.Actors).Where(x => x.Genres.Select(g => g.GenreName.ToLower()).Contains(Type.ToLower())).Select(x => new ContentWithType { Type = "film", Id = x.Id, Film = x }).ToListAsync();
            var querySeries = await _dbContext.Series.Include(x => x.Genres).Include(x => x.Actors).Include(x=>x.SeriesEpisodes).Where(x => x.Genres.Select(g => g.GenreName.ToLower()).Contains(Type.ToLower())).Select(x => new ContentWithType { Type = "series", Id = x.Id, Series = x }).ToListAsync();

            var query = queryFilm.Concat(querySeries).AsQueryable();

            return query.SingleOrDefault(c => c.Id == id);
        }
    }
}
