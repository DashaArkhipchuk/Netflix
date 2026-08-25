using MediatR;
using Microsoft.Extensions.Options;
using Netflix.Domain.DTOs.NewsApi;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using Netflix.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Commands.PopulateNews
{
    internal class PopulateNewsCommandHandler(
    INewsApiExternalVendorRepository newsApiRepository,
    INewsRepository newsRepository,
    IArticleContentScraperService scraper,
    INewsPopulationSettings settings) : IRequestHandler<PopulateNewsCommand, PopulateNewsResultDto>
    {
        private static readonly (string TypeName, string[] Keywords)[] Categories =
        {
        ("Movies",      new[] { "film", "movie", "cinema", "box office", "director", "sequel", "trailer" }),
        ("Celebrities", new[] { "celebrity", "celebrities", "star", "actor", "actress", "red carpet" }),
        ("Casting",     new[] { "casting", "joins cast", "cast announced", "role of" }),
    };
        private const string FallbackType = "Entertainment";

        public async Task<PopulateNewsResultDto> Handle(PopulateNewsCommand request, CancellationToken ct)
        {
            var pages = request.Pages ?? settings.DefaultPages;
            var pageSize = request.PageSize ?? settings.DefaultPageSize;

            var articles = await newsApiRepository.GetBulkArticlesAsync(pages, pageSize, ct);

            var typeCache = await newsRepository.GetNewsTypesAsync(ct);
            var authorCache = await newsRepository.GetAuthorsAsync(ct);
            var existingUrls = await newsRepository.GetExistingSourceUrlsAsync(ct);

            // Dedupe up front so we only scrape articles we're actually going to persist.
            var candidates = articles
                .Where(a => !string.IsNullOrWhiteSpace(a.Url) && !string.IsNullOrWhiteSpace(a.Title))
                .Where(a => existingUrls.Add(a.Url))
                .ToList();

            var skippedInvalid = articles.Count(a => string.IsNullOrWhiteSpace(a.Url) || string.IsNullOrWhiteSpace(a.Title));
            var skippedDuplicate = articles.Count - candidates.Count - skippedInvalid;

            var scrapedText = await ScrapeAllAsync(candidates, settings.MaxScrapeConcurrency, ct);

            var inserted = 0;
            foreach (var article in candidates)
            {
                var typeName = Classify(article);
                var type = GetOrCreateType(typeCache, typeName);

                var fullText = scrapedText.TryGetValue(article.Url, out var scraped) && !string.IsNullOrWhiteSpace(scraped)
                    ? scraped
                    : BuildFallbackText(article); // scrape failed — fall back to description + truncated snippet

                var news = new Domain.Entities.News
                {
                    Id = Guid.NewGuid(),
                    TypeId = type.Id,
                    Type = type,
                    Title = Truncate(article.Title, 500),
                    Description = article.Description ?? string.Empty,
                    ArticleText = fullText,
                    ImageUrl = article.UrlToImage ?? string.Empty,
                    PublishedDate = article.PublishedAt == default ? DateTime.UtcNow : article.PublishedAt.UtcDateTime,
                    ViewCount = 0,
                    SourceUrl = article.Url,
                    SourceName = article.SourceName,
                };

                foreach (var author in ParseAuthors(article.Author))
                {
                    var key = $"{author.Name}|{author.Surname}";
                    if (!authorCache.TryGetValue(key, out var authorEntity))
                    {
                        authorEntity = new AuthorModel { Id = Guid.NewGuid(), Name = author.Name, Surname = author.Surname };
                        authorCache[key] = authorEntity;
                        newsRepository.AddAuthor(authorEntity);
                    }
                    news.Authors.Add(authorEntity);
                }

                newsRepository.AddNews(news);
                inserted++;
            }

            await newsRepository.SaveChangesAsync(ct);

            return new PopulateNewsResultDto(articles.Count, inserted, skippedDuplicate, skippedInvalid);
        }

        // Throttled so we don't fire off hundreds of simultaneous requests at news sites.
        private async Task<Dictionary<string, string?>> ScrapeAllAsync(
            List<NewsArticleDto> candidates, int maxConcurrency, CancellationToken ct)
        {
            var results = new Dictionary<string, string?>(StringComparer.OrdinalIgnoreCase);
            using var throttle = new SemaphoreSlim(maxConcurrency);

            var tasks = candidates.Select(async article =>
            {
                await throttle.WaitAsync(ct);
                try
                {
                    var text = await scraper.ScrapeArticleTextAsync(article.Url, ct);
                    lock (results) { results[article.Url] = text; }
                }
                finally
                {
                    throttle.Release();
                }
            });

            await Task.WhenAll(tasks);
            return results;
        }

        private static string Classify(NewsArticleDto article)
        {
            var haystack = $"{article.Title} {article.Description}".ToLowerInvariant();
            foreach (var (typeName, keywords) in Categories)
            {
                if (keywords.Any(k => haystack.Contains(k, StringComparison.OrdinalIgnoreCase)))
                    return typeName;
            }
            return FallbackType;
        }

        private NewsType GetOrCreateType(Dictionary<string, NewsType> cache, string name)
        {
            if (cache.TryGetValue(name, out var existing))
                return existing;

            var created = new NewsType { Id = Guid.NewGuid(), Name = name };
            cache[name] = created;
            newsRepository.AddNewsType(created);
            return created;
        }

        private static IEnumerable<(string Name, string Surname)> ParseAuthors(string? rawAuthor)
        {
            if (string.IsNullOrWhiteSpace(rawAuthor))
                yield break;

            var cleaned = rawAuthor.Trim();
            if (cleaned.StartsWith("By ", StringComparison.OrdinalIgnoreCase))
                cleaned = cleaned[3..];

            foreach (var part in cleaned.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
            {
                if (part.Length > 100) continue;

                var tokens = part.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length == 0) continue;

                var surname = tokens.Length > 1 ? tokens[^1] : string.Empty;
                var name = tokens.Length > 1 ? string.Join(' ', tokens[..^1]) : tokens[0];

                yield return (name, surname);
            }
        }

        private static string Truncate(string value, int max) => value.Length <= max ? value : value[..max];

        private static string BuildFallbackText(NewsArticleDto article)
        {
            var content = article.Content?.Split("[+")[0].Trim();
            return string.Join("\n\n", new[] { article.Description, content }
                .Where(s => !string.IsNullOrWhiteSpace(s)));
        }
    }
}
