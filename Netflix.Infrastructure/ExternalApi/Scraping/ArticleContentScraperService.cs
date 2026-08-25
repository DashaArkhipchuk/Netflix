using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Netflix.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.ExternalApi.Scraping
{
    public class ArticleContentScraperService(HttpClient httpClient, ILogger<ArticleContentScraperService> logger)
        : IArticleContentScraperService
    {
        public async Task<string?> ScrapeArticleTextAsync(string url, CancellationToken ct = default)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                // Many sites block the default HttpClient user agent outright.
                request.Headers.UserAgent.ParseAdd(
                    "Mozilla/5.0 (compatible; NetflixNewsBot/1.0; +https://example.com/bot)");

                using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                cts.CancelAfter(TimeSpan.FromSeconds(10));

                var response = await httpClient.SendAsync(request, cts.Token);
                if (!response.IsSuccessStatusCode)
                {
                    logger.LogWarning("Scrape failed for {Url}: {Status}", url, response.StatusCode);
                    return null;
                }

                var html = await response.Content.ReadAsStringAsync(cts.Token);
                return ExtractMainText(html);
            }
            catch (Exception ex)
            {
                // Scraping is inherently unreliable (paywalls, JS-rendered content, blocks, timeouts).
                // Failure here must not take down the whole population run.
                logger.LogWarning(ex, "Scrape threw for {Url}", url);
                return null;
            }
        }

        private static string? ExtractMainText(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Strip elements that are never article body content.
            foreach (var node in doc.DocumentNode
                         .SelectNodes("//script|//style|//nav|//header|//footer|//aside|//form")
                         ?? new HtmlNodeCollection(null))
            {
                node.Remove();
            }

            // Prefer <article>, fall back to whichever container has the most paragraph text.
            var articleNode = doc.DocumentNode.SelectSingleNode("//article");
            var scope = articleNode ?? doc.DocumentNode;

            var paragraphs = scope.SelectNodes(".//p")
                ?.Select(p => HtmlEntity.DeEntitize(p.InnerText)?.Trim())
                .Where(t => !string.IsNullOrWhiteSpace(t) && t.Length > 40) // filters out captions/nav crumbs
                .ToList();

            if (paragraphs is null || paragraphs.Count == 0)
                return null;

            return string.Join("\n\n", paragraphs);
        }
    }
}
