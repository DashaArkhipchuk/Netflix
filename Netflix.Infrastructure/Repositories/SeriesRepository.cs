using Microsoft.EntityFrameworkCore;
using Netflix.Domain.IRepository;
using Netflix.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Logging;

namespace Netflix.Infrastructure.Repositories
{
    internal class SeriesRepository(NetflixProjectContext dbContext) : ISeriesRepository
    {

        public async Task<List<Series>> GetAllAsync(int skip, int take, List<string> genres, bool sortByLatest = false, decimal? minimumRating = null, int? year = null, int? episodes = null)
        {
            // Start with the base query
            var query = dbContext.Series.Include(s => s.Genres).AsQueryable();

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
            return await dbContext.Series.Include(s => s.Actors).Include(s => s.Genres).Include(s=>s.SeriesEpisodes.OrderBy(x=>x.EpisodeNumber)).SingleOrDefaultAsync(series => series.Id == id);
        }

        public async Task<string> PopulateSeriesEpisodes()
        {

            // Fetch all series from the database
            var seriesList = await dbContext.Series.ToListAsync();

            foreach (var series in seriesList)
            {

                await GenerateEpisodesForSeriesAsync(dbContext, series);


            }

            return "";

        }


        private static async Task GenerateEpisodesForSeriesAsync(NetflixProjectContext dbContext, Series series)
        {
            var words = new[] { "Shadow", "Legend", "Mystery", "Journey", "Rising", "Quest", "Echo", "Destiny", "Revenge", "Secret" };

            var seriesWords = series.Name.Split(' ');
            if (seriesWords.Length < 2) return;

            var firstWord = seriesWords[0];
            var secondWord = seriesWords[1];

            var firstWordStartIndex = words
                .Select((word, idx) => new { Word = word, Index = idx }) // Select word with index
                .Where(x => x.Word == firstWord) // Filter by target word
                .Select(x => x.Index) // Select index
                .FirstOrDefault();

            var secondWordStartIndex = words
                .Select((word, idx) => new { Word = word, Index = idx }) // Select word with index
                .Where(x => x.Word == secondWord) // Filter by target word
                .Select(x => x.Index) // Select index
                .FirstOrDefault();

            var episodeCount = series.EpisodeCount;
            var seasonCount = series.SeasonCount;

            int episodesPerSeason = (int)Math.Floor((double)episodeCount / seasonCount);
            int currentSeason = 1;
            int currentEpisode = 1;
            int currentEpisodeInSeason = 1;


            while (currentEpisode <= episodeCount)
            {
                int firstWordIndex = firstWordStartIndex;
                int i = 1;
                while (true) // Infinite loop for the first word
                {
                    string first = words[firstWordIndex];

                    int secondWordIndex = 0;
                    if (i == 1)
                    {
                        secondWordIndex = secondWordStartIndex;
                    } 
                    while (true) // Infinite loop for the second word
                    {
                        string second = words[secondWordIndex];

                        // Combine the first and second words
                        string combinedName = first + second;
                        var videoUrl = $"https://netflixmediastorage.blob.core.windows.net/videos/{combinedName}.mp4";
                        var pictureUrl = $"https://netflixmediastorage.blob.core.windows.net/images/{combinedName}.jpg";

                        // Create the episode object
                        var episode = new SeriesEpisode
                        {
                            SeriesId = series.Id,
                            EpisodeName = combinedName,
                            SeasonNumber = currentSeason,
                            EpisodeNumber = currentEpisode,
                            EpisodeNumberInSeason = currentEpisodeInSeason,
                            PictureURL = pictureUrl,
                            VideoURL = videoUrl
                        };

                        // Add the episode to the context
                        dbContext.SeriesEpisode.Add(episode);

                        // Increment episode and handle season transitions
                        currentEpisode++;
                        currentEpisodeInSeason++;
                        if (currentEpisode > episodesPerSeason * currentSeason)
                        {
                            if (currentSeason < seasonCount)
                            {
                                currentEpisodeInSeason = 1;
                                currentSeason++;
                            }
                        }
                        if (currentEpisode > episodeCount)
                        {
                            await dbContext.SaveChangesAsync();
                            return;
                        }

                        i++;

                        if (secondWordIndex == words.Length - 1)
                            break;

                        // Move to the next second word
                        secondWordIndex = (secondWordIndex + 1) % words.Count();

                        // Break the inner loop after cycling through all second words

                    }

                    // Move to the next first word
                    firstWordIndex = (firstWordIndex + 1) % words.Count();

                    // Optionally break the outer loop based on a condition
                    // For example, you can break after generating a certain number of combinations
                    // if (someCondition) break;
                }
            }

        }
    }
}
