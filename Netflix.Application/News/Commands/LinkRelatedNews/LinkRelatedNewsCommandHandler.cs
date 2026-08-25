using MediatR;
using Netflix.Application.News.Common;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Commands.LinkRelatedNews
{
    internal class LinkRelatedNewsCommandHandler(
        INewsRepository newsRepository,
        IRelatedNewsClassifier classifier) : IRequestHandler<LinkRelatedNewsCommand, LinkRelatedNewsResultDto>
    {
        private const int DefaultMaxRelatedPerArticle = 5;
        private const double DefaultMinSimilarity = 0.08;

        public async Task<LinkRelatedNewsResultDto> Handle(LinkRelatedNewsCommand request, CancellationToken ct)
        {
            var items = await newsRepository.GetNewsForClassificationAsync(ct);
            if (items.Count < 2)
            {
                return new LinkRelatedNewsResultDto(
                    CandidatesFound: 0,
                    Linked: 0,
                    SkippedExisting: 0,
                    TotalArticles: items.Count,
                    ArticleCountByType: new Dictionary<Guid, int>(),
                    ArticlesWithZeroRelations: items.Count,
                    AverageRelationsPerArticle: 0,
                    MaxRelationsForAnyArticle: 0);
            }

            var candidates = classifier.FindRelated(
                items,
                request.MaxRelatedPerArticle ?? DefaultMaxRelatedPerArticle,
                request.MinSimilarity ?? DefaultMinSimilarity);

            var existingPairs = await newsRepository.GetExistingRelationPairsAsync(ct);

            var linked = 0;
            var skipped = 0;

            foreach (var (newsId, relatedNewsId, _) in candidates)
            {
                if (existingPairs.Contains((newsId, relatedNewsId)))
                {
                    skipped++;
                    continue;
                }

                newsRepository.AddNewsRelation(newsId, relatedNewsId);
                existingPairs.Add((newsId, relatedNewsId)); // guard against dupes within this same run
                linked++;
            }

            if (linked > 0)
                await newsRepository.SaveChangesAsync(ct);

            var countByType = items
                .GroupBy(i => i.TypeId)
                .ToDictionary(g => g.Key, g => g.Count());

            var degreeCount = new Dictionary<Guid, int>();
            foreach (var (newsId, relatedNewsId, _) in candidates)
            {
                degreeCount[newsId] = degreeCount.GetValueOrDefault(newsId) + 1;
                degreeCount[relatedNewsId] = degreeCount.GetValueOrDefault(relatedNewsId) + 1;
            }

            var zeroRelationArticles = items.Count(i => !degreeCount.ContainsKey(i.Id));
            var avgRelations = degreeCount.Count > 0 ? degreeCount.Values.Average() : 0;
            var maxRelations = degreeCount.Count > 0 ? degreeCount.Values.Max() : 0;

            return new LinkRelatedNewsResultDto(
                CandidatesFound: candidates.Count,
                Linked: linked,
                SkippedExisting: skipped,
                TotalArticles: items.Count,
                ArticleCountByType: countByType,
                ArticlesWithZeroRelations: zeroRelationArticles,
                AverageRelationsPerArticle: avgRelations,
                MaxRelationsForAnyArticle: maxRelations);
        }
    }
}
