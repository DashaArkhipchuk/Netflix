using Netflix.Domain.DTOs.News;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.News.Common
{

    public class RelatedNewsClassifier : IRelatedNewsClassifier
    {
        private static readonly HashSet<string> StopWords = new(StringComparer.OrdinalIgnoreCase)
    {
        "the","a","an","and","or","but","of","in","on","at","to","for","with","by","from",
        "is","are","was","were","be","been","being","it","its","this","that","these","those",
        "as","said","says","will","has","have","had","not","new","after","over","into","out",
        "up","down","about","also","more","than","who","what","when","where","how","just"
    };

        public IReadOnlyList<(Guid NewsId, Guid RelatedNewsId, double Score)> FindRelated(
            IReadOnlyList<NewsClassificationItem> items,
            int maxRelatedPerArticle,
            double minSimilarity)
        {
            var results = new List<(Guid NewsId, Guid RelatedNewsId, double Score)>();

            // never relate articles across different NewsTypes.
            foreach (var group in items.GroupBy(i => i.TypeId))
            {
                var groupItems = group.ToList();
                if (groupItems.Count < 2)
                    continue; // nothing to relate a single article to within its own type

                results.AddRange(ClassifyGroup(groupItems, maxRelatedPerArticle, minSimilarity));
            }

            return results;
        }

        private static IEnumerable<(Guid, Guid, double)> ClassifyGroup(
            List<NewsClassificationItem> groupItems, int maxRelatedPerArticle, double minSimilarity)
        {
            var keywordSets = groupItems.ToDictionary(i => i.Id, Tokenize);

            // Drop keywords that appear in too many articles within the group, they carry no discriminating signal (crude IDF-style filter without a full NLP library).
            var docFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (var set in keywordSets.Values)
                foreach (var kw in set)
                    docFrequency[kw] = docFrequency.GetValueOrDefault(kw) + 1;

            var maxDocFraction = 0.75;
            var tooCommon = docFrequency
                .Where(kv => (double)kv.Value / groupItems.Count > maxDocFraction)
                .Select(kv => kv.Key)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var id in keywordSets.Keys.ToList())
                keywordSets[id] = keywordSets[id].Where(k => !tooCommon.Contains(k)).ToHashSet(StringComparer.OrdinalIgnoreCase);

            var perArticleBest = new Dictionary<Guid, List<(Guid OtherId, double Score)>>();
            var allIds = keywordSets.Keys.ToList();

            foreach (var id in allIds)
            {
                var keywords = keywordSets[id];
                // Compare against every other article in the group, not just keyword-overlap candidates —
                // near-universal coverage means we can't skip comparisons just because overlap is zero;
                // a zero-overlap pair might still be this article's "best available" match.
                var scored = allIds
                    .Where(otherId => otherId != id)
                    .Select(otherId => (OtherId: otherId, Score: Jaccard(keywords, keywordSets[otherId])))
                    .OrderByDescending(s => s.Score)
                    .ToList();

                // Primary tier: matches that clear the quality bar.
                var qualified = scored.Where(s => s.Score >= minSimilarity).Take(maxRelatedPerArticle).ToList();

                // Fallback tier: if too few qualified matches, fill remaining slots with the best
                // available regardless of score — this is what makes coverage near-universal.
                // These are still ranked (best-first), just not gated by the threshold.
                if (qualified.Count < maxRelatedPerArticle)
                {
                    var alreadyPicked = qualified.Select(q => q.OtherId).ToHashSet();
                    var fillers = scored
                        .Where(s => !alreadyPicked.Contains(s.OtherId) && s.Score > 0) // score > 0 = at least one shared word
                        .Take(maxRelatedPerArticle - qualified.Count);
                    qualified.AddRange(fillers);
                }

                perArticleBest[id] = qualified;
            }

            var seenPairs = new HashSet<(Guid, Guid)>();
            foreach (var (id, matches) in perArticleBest)
            {
                foreach (var (otherId, score) in matches)
                {
                    var pair = id.CompareTo(otherId) < 0 ? (id, otherId) : (otherId, id);
                    if (seenPairs.Add(pair))
                        yield return (pair.Item1, pair.Item2, score);
                }
            }
        }

        private static double Jaccard(HashSet<string> a, HashSet<string> b)
        {
            if (a.Count == 0 || b.Count == 0) return 0;
            var intersection = a.Intersect(b, StringComparer.OrdinalIgnoreCase).Count();
            var union = a.Union(b, StringComparer.OrdinalIgnoreCase).Count();
            return union == 0 ? 0 : (double)intersection / union;
        }

        private static HashSet<string> Tokenize(NewsClassificationItem item)
        {
            // Weight title higher since it's the most topic-dense text we have
            var text = $"{item.Title} {item.Title} {item.Description} {item.ArticleTextExcerpt}";
            return text
                .Split(new[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ':', ';', '"', '(', ')', '\'' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToLowerInvariant())
                .Where(w => w.Length > 2 && !StopWords.Contains(w) && !int.TryParse(w, out _))
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
    }
}
