using Netflix.Domain.DTOs.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Common
{
    public interface IRelatedNewsClassifier
    {
        // Returns canonicalized, deduplicated, self-pair-free candidate relations, already
        // capped to maxRelatedPerArticle per article and filtered to minSimilarity.
        IReadOnlyList<(Guid NewsId, Guid RelatedNewsId, double Score)> FindRelated(
            IReadOnlyList<NewsClassificationItem> items,
            int maxRelatedPerArticle,
            double minSimilarity);
    }
}
