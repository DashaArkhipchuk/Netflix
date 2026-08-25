using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Commands.LinkRelatedNews
{
    public record LinkRelatedNewsResultDto(
        int CandidatesFound,
        int Linked,
        int SkippedExisting,
        int TotalArticles,
        Dictionary<Guid, int> ArticleCountByType,
        int ArticlesWithZeroRelations, // should be 0 if "universal" is working
        double AverageRelationsPerArticle,
        int MaxRelationsForAnyArticle
     );
}
