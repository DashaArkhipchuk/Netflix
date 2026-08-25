using Microsoft.EntityFrameworkCore;
using Netflix.Domain.DTOs.Common;
using Netflix.Domain.DTOs.News;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Netflix.Infrastructure.Repositories
{
    internal class NewsRepository(NetflixProjectContext dbContext) : INewsRepository
    {
        public async Task<PagedResult<News>> GetAllAsync(int skip, int take, NewsFilter? filter, CancellationToken ct = default)
        {
            var query = dbContext.News
                .Include(n => n.Authors)
                .Include(n => n.Type)
                .AsQueryable();

            if (filter?.TypeId is { } typeId)
                query = query.Where(n => n.TypeId == typeId);

            if (filter?.AuthorIds is { Count: > 0 } authorIds)
                query = query.Where(n => n.Authors.Any(a => authorIds.Contains(a.Id)));

            if (!string.IsNullOrWhiteSpace(filter?.Search))
            {
                var term = filter.Search.Trim();
                query = query.Where(n => n.Title.Contains(term) || n.Description.Contains(term));
            }

            query = filter?.SortBy switch
            {
                NewsSortOption.MostViewed => query.OrderByDescending(n => n.ViewCount),
                NewsSortOption.TitleAsc => query.OrderBy(n => n.Title),
                _ => query.OrderByDescending(n => n.PublishedDate), // Latest is the default
            };

            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip(skip).Take(take).ToListAsync(ct);

            return new PagedResult<News> { Items = items, TotalCount = totalCount };
        }

        public async Task<News?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
            await dbContext.News
                .Include(n => n.Type)
                .Include(n => n.Authors)
                .SingleOrDefaultAsync(n => n.Id == id, ct);

        public async Task<List<(NewsType Type, int Count)>> GetAllNewsTypesWithCountsAsync(CancellationToken ct = default)
        {
            return await dbContext.NewsTypes
                .Select(t => new { Type = t, NewsCount = dbContext.News.Count(n => n.TypeId == t.Id) })
                .Select(x => new Tuple<NewsType, int>(x.Type, x.NewsCount).ToValueTuple())
                .ToListAsync(ct);
        }

        public async Task IncrementViewCountAsync(Guid id, CancellationToken ct = default)
        {
            await dbContext.News
                .Where(n => n.Id == id)
                .ExecuteUpdateAsync(setters => setters.SetProperty(n => n.ViewCount, n => n.ViewCount + 1), ct);
        }

        public async Task<Dictionary<string, NewsType>> GetNewsTypesAsync(CancellationToken ct = default) =>
            await dbContext.NewsTypes.ToDictionaryAsync(t => t.Name, StringComparer.OrdinalIgnoreCase, ct);

        public async Task<Dictionary<string, AuthorModel>> GetAuthorsAsync(CancellationToken ct = default)
        {
            var authors = await dbContext.Authors.ToListAsync(ct);

            // GroupBy + first-wins instead of ToDictionary: tolerates pre-existing duplicate
            // (Name, Surname) rows in the DB instead of crashing the whole populate run over data
            // that's already there. This masks the symptom — see Fix 2 for the actual prevention.
            return authors
                .GroupBy(a => Key(a.Name, a.Surname), StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
        }

        public async Task<HashSet<string>> GetExistingSourceUrlsAsync(CancellationToken ct = default) =>
            (await dbContext.News.Select(n => n.SourceUrl).ToListAsync(ct))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

        public void AddNewsType(NewsType type) => dbContext.NewsTypes.Add(type);

        public void AddAuthor(AuthorModel author) => dbContext.Authors.Add(author);

        public void AddNews(News news) => dbContext.News.Add(news);

        public Task SaveChangesAsync(CancellationToken ct = default) => dbContext.SaveChangesAsync(ct);

        private static string Key(string name, string surname) =>
            $"{name.Trim()}|{surname.Trim()}";

        public async Task<List<NewsClassificationItem>> GetNewsForClassificationAsync(CancellationToken ct = default) =>
            await dbContext.News
            .Select(n => new NewsClassificationItem
            {
                Id = n.Id,
                TypeId = n.TypeId,
                Title = n.Title,
                Description = n.Description,
                ArticleTextExcerpt = n.ArticleText.Length > 500 ? n.ArticleText.Substring(0, 500) : n.ArticleText
            })
            .ToListAsync(ct);

        public async Task<HashSet<(Guid NewsId, Guid RelatedNewsId)>> GetExistingRelationPairsAsync(CancellationToken ct = default)
        {
            return (await dbContext.Set<NewsRelated>()
                .Select(r => new { r.NewsId, r.RelatedNewsId })
                .ToListAsync(ct))
            .Select(r => (r.NewsId, r.RelatedNewsId)).ToHashSet();
        }

        public void AddNewsRelation(Guid newsId, Guid relatedNewsId) =>
            dbContext.Set<NewsRelated>().Add(new NewsRelated { NewsId = newsId, RelatedNewsId = relatedNewsId });

        public async Task<PagedResult<News>> GetRelatedNewsAsync(Guid newsId, int skip, int take, CancellationToken ct = default)
        {
            // Canonical storage means a relation could be stored with newsId on either side, query both directions to reassemble the full "related to this article" set.
            var relatedIds = await dbContext.Set<NewsRelated>()
                .Where(r => r.NewsId == newsId || r.RelatedNewsId == newsId)
                .Select(r => r.NewsId == newsId ? r.RelatedNewsId : r.NewsId)
                .ToListAsync(ct);

            var query = dbContext.News
                .Include(n => n.Authors)
                .Where(n => relatedIds.Contains(n.Id));

            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip(skip).Take(take).ToListAsync(ct);

            return new PagedResult<News> { Items = items, TotalCount = totalCount };
        }
    }
}
