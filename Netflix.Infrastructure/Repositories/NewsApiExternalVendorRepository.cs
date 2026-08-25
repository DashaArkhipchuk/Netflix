using Netflix.Domain.DTOs.NewsApi;
using Netflix.Domain.IRepository;
using Netflix.Infrastructure.ExternalApi.NewsApi;
using Netflix.Infrastructure.ExternalApi.NewsApi.Dtos;
using System.Collections.Generic;


namespace Netflix.Infrastructure.Repositories
{

    public class NewsApiExternalVendorRepository(INewsApiHttpClientService client) : INewsApiExternalVendorRepository
    {
        private const string Query =
            "movie OR movies OR film OR films OR cinema OR celebrity OR celebrities " +
            "OR actor OR actress OR hollywood OR casting OR \"box office\"";

        // Developer-plan hard ceiling
        private const int MaxResultsAllowedByPlan = 100;

        public async Task<List<NewsArticleDto>> GetBulkArticlesAsync(int pages, int pageSize, CancellationToken ct = default)
        {
            var articles = new List<NewsArticleDto>();

            for (var page = 1; page <= pages; page++)
            {
                // Never request beyond result 100,  the API rejects that outright on free plan.
                var resultsRequestedSoFar = (page - 1) * pageSize;
                if (resultsRequestedSoFar >= MaxResultsAllowedByPlan)
                    break;

                // Clamp pageSize on the final partial page so we land exactly on the ceiling, not past it.
                var remaining = MaxResultsAllowedByPlan - resultsRequestedSoFar;
                var effectivePageSize = Math.Min(pageSize, remaining);

                var result = await client.GetEverythingAsync(Query, page, effectivePageSize, ct);
                if (result?.Articles is null || result.Articles.Count == 0)
                    break;

                articles.AddRange(result.Articles.Select(MapToDomain));

                if (result.Articles.Count < effectivePageSize)
                    break; // fewer results than asked for, nothing more to fetch
            }

            return articles;
        }

        private static NewsArticleDto MapToDomain(ArticleDto a) => new()
        {
            SourceId = a.Source?.Id,
            SourceName = a.Source?.Name,
            Author = a.Author,
            Title = a.Title,
            Description = a.Description,
            Url = a.Url,
            UrlToImage = a.UrlToImage,
            PublishedAt = a.PublishedAt,
            Content = a.Content,
        };
    }
}
