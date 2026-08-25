using Netflix.Domain.DTOs.Common;
using Netflix.Domain.DTOs.News;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Domain.IRepository
{
    public interface INewsRepository
    {
        Task<PagedResult<News>> GetAllAsync(int skip, int take, NewsFilter? filter, CancellationToken ct = default);
        Task<News?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<(NewsType Type, int Count)>> GetAllNewsTypesWithCountsAsync(CancellationToken ct = default);
        Task IncrementViewCountAsync(Guid id, CancellationToken ct = default);

        Task<Dictionary<string, NewsType>> GetNewsTypesAsync(CancellationToken ct = default);
        Task<Dictionary<string, AuthorModel>> GetAuthorsAsync(CancellationToken ct = default);
        Task<HashSet<string>> GetExistingSourceUrlsAsync(CancellationToken ct = default);

        void AddNewsType(NewsType type);
        void AddAuthor(AuthorModel author);
        void AddNews(News news);

        Task<List<NewsClassificationItem>> GetNewsForClassificationAsync(CancellationToken ct = default);
        Task<HashSet<(Guid NewsId, Guid RelatedNewsId)>> GetExistingRelationPairsAsync(CancellationToken ct = default);
        void AddNewsRelation(Guid newsId, Guid relatedNewsId);
        Task<PagedResult<News>> GetRelatedNewsAsync(Guid newsId, int skip, int take, CancellationToken ct = default);


        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
